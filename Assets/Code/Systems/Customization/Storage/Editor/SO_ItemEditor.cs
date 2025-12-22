using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [CustomEditor(typeof(SO_Item))]
    public class SO_ItemEditor : Editor
    {
        private SerializedProperty _library, _category, _label, _price;
        private string[] _categories = new string[0];
        private string[] _labels = new string[0];

        private void OnEnable()
        {
            _library = serializedObject.FindProperty("_assetReference");
            _category = serializedObject.FindProperty("_category");
            _label = serializedObject.FindProperty("_label");
            _price = serializedObject.FindProperty("_price");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_library);

            var library = _library.objectReferenceValue as SpriteLibraryAsset;
            if (library == null)
            {
                EditorGUILayout.HelpBox("Asigna un SpriteLibraryAsset", MessageType.Info);
                serializedObject.ApplyModifiedProperties();
                return;
            }

            DrawCategoryDropdown(library);
            DrawLabelDropdown(library);
            DrawPriceField();

            DrawSpritePreview(library);

            serializedObject.ApplyModifiedProperties();
        }
        public override Texture2D RenderStaticPreview(string assetPath, UnityEngine.Object[] subAssets, int width, int height)
        {
            var item = target as SO_Item;
            var sprite = item.Sprite;

            if (sprite == null) base.RenderStaticPreview(assetPath, subAssets, width, height);

            //asign texture in object
            Texture2D previewTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            EditorUtility.CopySerialized(sprite.texture, previewTexture);
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
            _price.intValue = EditorGUILayout.IntField("Price", _price.intValue);
        }
    }
}