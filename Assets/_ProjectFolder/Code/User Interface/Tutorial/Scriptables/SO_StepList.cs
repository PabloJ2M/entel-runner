using UnityEngine;

namespace Unity.Tutorial
{
    [CreateAssetMenu(fileName = "tutorial", menuName = "tutorial/step list", order = 0)]
    public class SO_StepList : ScriptableObject
    {
        [SerializeField] private SO_Step[] _steps;

        public SO_Step this[int index] => _steps[index];
        public int Length => _steps.Length;
    }
}