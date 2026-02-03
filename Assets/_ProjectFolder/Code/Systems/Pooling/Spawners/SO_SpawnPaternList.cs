using UnityEngine;

namespace Unity.Pool
{
    [CreateAssetMenu(fileName = "SO_SpawnPaternList", menuName = "Scriptable Objects/SO_SpawnPaternList")]
    public class SO_SpawnPaternList : ScriptableObject
    {
        [SerializeField] private SO_SpawnPatern[] _patenrs;

        public SO_SpawnPatern GetRandomPattern(int dificulty)
        {
            int length = Mathf.Min(dificulty, _patenrs.Length);
            return _patenrs[Random.Range(0, length)];
        }
    }
}