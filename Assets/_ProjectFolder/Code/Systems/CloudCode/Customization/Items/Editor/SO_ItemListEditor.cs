using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Unity.Customization
{
    [CustomEditor(typeof(SO_ItemList))]
    public class SO_ItemListEditor : Editor
    {
        private SerializedProperty _items;
        private string _customPath;

        private void OnEnable() => _items = serializedObject.FindProperty("_items");

        public override void OnInspectorGUI()
        {
            _customPath = EditorGUILayout.TextField("Custom Path", _customPath);
            if (GUILayout.Button("Load Scriptables"))
                LoadAllScriptables();

            EditorGUILayout.Space();
            base.OnInspectorGUI();
        }

        private void LoadAllScriptables()
        {
            string[] guids = string.IsNullOrEmpty(_customPath) ?
                AssetDatabase.FindAssets("t:SO_Item") :
                AssetDatabase.FindAssets("t:SO_Item", new string[] { $"Assets/{_customPath}" });

            var loadedItems = guids
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                .Select(path => AssetDatabase.LoadAssetAtPath<SO_Item>(path))
                .Where(item => item != null)
                .ToArray();

            Undo.RecordObject(target, "Load Scriptables");
            
            _items.ClearArray();
            _items.arraySize = loadedItems.Length;
            
            for (int i = 0; i < loadedItems.Length; i++)
                _items.GetArrayElementAtIndex(i).objectReferenceValue = loadedItems[i];

            serializedObject.ApplyModifiedProperties();
        }
    }
}