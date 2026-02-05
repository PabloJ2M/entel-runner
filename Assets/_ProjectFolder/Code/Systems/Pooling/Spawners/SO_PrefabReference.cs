using UnityEngine;

namespace Unity.Pool
{
    [CreateAssetMenu(fileName = "Prefab Group"/*, menuName = ""*/)]
    public class SO_PrefabReference : ScriptableObject
    {
        [SerializeField] private string[] _prefabs;
        private int _index;

        public string GetRandom() => _prefabs[Random.Range(0, _prefabs.Length)];
        public string GetSequence()
        {
            _index++;
            _index %= _prefabs.Length;
            return _prefabs[_index];
        }
    }
}