using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Unity.Customization
{
    [CustomEditor(typeof(SO_Item_Container))]
    public class SO_ItemListEditor : Editor
    {
        private SerializedProperty _keys, _values;
        private string _customPath;

        private void OnEnable()
        {
            var items = serializedObject.FindProperty("_items");
            _keys = items.FindPropertyRelative("m_Keys");
            _values = items.FindPropertyRelative("m_Values");
        }

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
                .Where(item => item != null);

            var groups = loadedItems
                .Where(item => item.Reference != null)
                .GroupBy(item => item.Reference);

            Undo.RecordObject(target, "Load Scriptables");

            _keys.ClearArray();
            _values.ClearArray();

            int i = 0;
            foreach (var group in groups)
            {
                _keys.InsertArrayElementAtIndex(i);
                _values.InsertArrayElementAtIndex(i);

                _keys.GetArrayElementAtIndex(i).objectReferenceValue = group.Key;

                var values = _values.GetArrayElementAtIndex(i).FindPropertyRelative("items");
                values.arraySize = group.Count();

                int j = 0;
                foreach (var item in group) {
                    values.GetArrayElementAtIndex(j).objectReferenceValue = item;
                    j++;
                }

                i++;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}