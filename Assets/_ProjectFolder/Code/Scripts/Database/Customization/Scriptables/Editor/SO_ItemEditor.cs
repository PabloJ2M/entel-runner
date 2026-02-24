using UnityEditor;
using UnityEngine;

namespace Unity.Customization
{
    [CustomEditor(typeof(SO_Item))]
    public class SO_ItemEditor : Editor
    {
        private SerializedProperty _library, _id;
        private SerializedProperty _sorting, _group, _label;
        private SerializedProperty _preview;
        private SerializedProperty _quality, _balance, _cost;

        private void OnEnable()
        {
            _library = serializedObject.FindProperty("_libraryReference");
            _id = serializedObject.FindProperty("_itemID");

            _sorting = serializedObject.FindProperty("_sorting");
            _group = serializedObject.FindProperty("_group");
            _label = serializedObject.FindProperty("_label");
            _preview = serializedObject.FindProperty("_preview");

            _quality = serializedObject.FindProperty("_quality");
            _balance = serializedObject.FindProperty("_balance");
            _cost = serializedObject.FindProperty("_cost");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            SO_Item item = target as SO_Item;

            EditorGUILayout.PropertyField(_library);
            _id.stringValue = EditorGUILayout.TextField("ID", _id.stringValue);
            EditorGUILayout.Space(10);

            if (item.Reference == null) {
                EditorGUILayout.HelpBox("Asigna un LibraryReference", MessageType.Info);
                return;
            }

            EditorGUILayout.PropertyField(_sorting);
            EditorGUILayout.PropertyField(_group);
            EditorGUILayout.PropertyField(_label);

            EditorGUILayout.PropertyField(_preview);
            DrawSpritePreview(item.Preview);

            EditorGUILayout.Space(10);
            EditorGUILayout.PropertyField(_quality);
            EditorGUILayout.PropertyField(_balance);
            EditorGUILayout.PropertyField(_cost);

            serializedObject.ApplyModifiedProperties();
        }
        private void DrawSpritePreview(Sprite sprite)
        {
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

        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            var item = target as SO_Item;
            if (item == null) return base.RenderStaticPreview(assetPath, subAssets, width, height);
            if (item.Preview == null) return base.RenderStaticPreview(assetPath, subAssets, width, height);

            //asign texture in object
            RenderTexture rt = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);
            Graphics.Blit(item.Preview.texture, rt);
            RenderTexture.active = rt;

            Texture2D previewTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            previewTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            previewTexture.Apply();

            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(rt);

            return previewTexture;
        }
    }
}