//using System;
//using System.Linq;
//using System.Collections.Generic;
using UnityEditor;
//using UnityEditorInternal;
using UnityEngine;
//using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [CustomEditor(typeof(SO_Item))]
    public class SO_ItemEditor : Editor
    {
        private SerializedProperty _library, _id;
        private SerializedProperty _group, /*_categories,*/ _label;
        private SerializedProperty _preview;
        private SerializedProperty _quality, _balance, _cost;

        //private ReorderableList _reorderableList;
        //private string[] _availableCategories = new string[0];

        private void OnEnable()
        {
            _library = serializedObject.FindProperty("_libraryReference");
            _id = serializedObject.FindProperty("_itemID");

            _group = serializedObject.FindProperty("_group");
            _label = serializedObject.FindProperty("_label");
            _preview = serializedObject.FindProperty("_preview");
            //_categories = serializedObject.FindProperty("_categories");

            _quality = serializedObject.FindProperty("_quality");
            _balance = serializedObject.FindProperty("_balance");
            _cost = serializedObject.FindProperty("_cost");
            
            //_reorderableList = new(serializedObject, _categories, true, true, true, true);
            //_reorderableList.drawHeaderCallback = rect => { EditorGUI.LabelField(rect, "Categories (drag to reorder)"); };
            //_reorderableList.drawElementCallback = (rect, index, active, focused) =>
            //{
            //    var element = _categories.GetArrayElementAtIndex(index);

            //    if (_availableCategories.Length == 0) {
            //        EditorGUI.LabelField(rect, "No categories available");
            //        return;
            //    }

            //    int currentIndex = Mathf.Max(0, Array.IndexOf(_availableCategories, element.stringValue));
            //    int newIndex = EditorGUI.Popup(rect, currentIndex, _availableCategories);
            //    element.stringValue = _availableCategories[newIndex];
            //};
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

            EditorGUILayout.PropertyField(_group);
            EditorGUILayout.PropertyField(_label);

            EditorGUILayout.PropertyField(_preview);
            DrawSpritePreview(item.Preview);
            //DrawCategoryDropdown(item.Reference.Asset);

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

        public override Texture2D RenderStaticPreview(string assetPath, /*UnityEngine.*/Object[] subAssets, int width, int height)
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

        //private void DrawCategoryDropdown(SpriteLibraryAsset library)
        //{
        //    _availableCategories = library.GetCategoryNames().ToArray();
        //    _reorderableList.DoLayoutList();

        //    string label = _label.stringValue;
        //    if (string.IsNullOrEmpty(label)) return;

        //    List<string> missing = new();

        //    for (int i = 0; i < _categories.arraySize; i++) {
        //        string cat = _categories.GetArrayElementAtIndex(i).stringValue;

        //        var sprite = library.GetSprite(cat, label);
        //        if (sprite == null) missing.Add(cat);
        //    }

        //    if (missing.Count > 0) {
        //        EditorGUILayout.HelpBox(
        //            $"Missing label '{label}' in categories:\n- " + string.Join("\n- ", missing),
        //            MessageType.Error
        //        );
        //    }
        //}
    }
}