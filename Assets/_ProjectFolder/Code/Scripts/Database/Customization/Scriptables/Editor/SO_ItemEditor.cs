using System;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEditor;

namespace Unity.Customization
{
    [CustomEditor(typeof(SO_Item))]
    public class SO_ItemEditor : Editor
    {
        private SerializedProperty _libraryReference, _id;
        private SerializedProperty _category, _label, _type, _cost;
        private string[] _categories = new string[0];
        private string[] _labels = new string[0];

        private void OnEnable()
        {
            _libraryReference = serializedObject.FindProperty("_libraryReference");
            _id = serializedObject.FindProperty("_itemID");
            _category = serializedObject.FindProperty("_category");
            _label = serializedObject.FindProperty("_label");

            _type = serializedObject.FindProperty("_type");
            _cost = serializedObject.FindProperty("_cost");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _libraryReference.objectReferenceValue = EditorGUILayout.ObjectField("Library Reference", _libraryReference.objectReferenceValue, typeof(SO_LibraryReference), false);
            _id.stringValue = EditorGUILayout.TextField("ID", _id.stringValue);

            SO_LibraryReference reference = _libraryReference.objectReferenceValue as SO_LibraryReference;

            if (reference == null) {
                EditorGUILayout.HelpBox("Asigna un LibraryReference", MessageType.Info);
                return;
            }

            DrawCategoryDropdown(reference.Asset);
            DrawLabelDropdown(reference.Asset);
            DrawPriceField();

            DrawSpritePreview(reference.Asset);
            serializedObject.ApplyModifiedProperties();
        }
        public override Texture2D RenderStaticPreview(string assetPath, UnityEngine.Object[] subAssets, int width, int height)
        {
            var item = target as SO_Item;
            if (item == null) return base.RenderStaticPreview(assetPath, subAssets, width, height);

            var sprite = item.Sprite;
            if (sprite == null) return base.RenderStaticPreview(assetPath, subAssets, width, height);
            if (sprite.texture == null) return base.RenderStaticPreview(assetPath, subAssets, width, height);

            //asign texture in object
            RenderTexture rt = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);
            Graphics.Blit(sprite.texture, rt);
            RenderTexture.active = rt;

            Texture2D previewTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            previewTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            previewTexture.Apply();

            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(rt);

            return previewTexture;
        }

        private void DrawCategoryDropdown(SpriteLibraryAsset library)
        {
            _categories = library.GetCategoryNames().ToArray();

            int currentIndex = Array.IndexOf(_categories, _category.stringValue);
            if (currentIndex < 0) SetIndex(currentIndex = 0);

            int newIndex = EditorGUILayout.Popup("Category", currentIndex, _categories);
            if (newIndex != currentIndex) SetIndex(newIndex);

            void SetIndex(int index)
            {
                _category.stringValue = _categories[index];
                _label.stringValue = string.Empty;
            }
        }
        private void DrawLabelDropdown(SpriteLibraryAsset library)
        {
            if (string.IsNullOrEmpty(_category.stringValue)) return;

            _labels = library.GetCategoryLabelNames(_category.stringValue).ToArray();

            int currentIndex = Mathf.Max(0, Array.IndexOf(_labels, _label.stringValue));
            int newIndex = EditorGUILayout.Popup("Label", currentIndex, _labels);

            _label.stringValue = _labels.Length > 0 ? _labels[newIndex] : string.Empty;
        }
        private void DrawSpritePreview(SpriteLibraryAsset library)
        {
            if (string.IsNullOrEmpty(_category.stringValue) || string.IsNullOrEmpty(_label.stringValue)) return;

            var sprite = library.GetSprite(_category.stringValue, _label.stringValue);
            if (sprite == null) return;
            if (sprite.texture == null) return;

            GUILayout.Space(10);
            GUILayout.Label("Preview", EditorStyles.boldLabel);

            Rect rect = GUILayoutUtility.GetRect(128, 128, GUILayout.ExpandWidth(false));

            Rect texCoords = new Rect(
                sprite.rect.x / sprite.texture.width,
                sprite.rect.y / sprite.texture.height,
                sprite.rect.width / sprite.texture.width,
                sprite.rect.height / sprite.texture.height);

            GUI.DrawTextureWithTexCoords(rect, sprite.texture, texCoords);
        }
        private void DrawPriceField()
        {
            EditorGUILayout.PropertyField(_type);
            _cost.intValue = EditorGUILayout.IntField("Cost", _cost.intValue);
        }
    }
}