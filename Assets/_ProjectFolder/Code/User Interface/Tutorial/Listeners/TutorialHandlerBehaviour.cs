using UnityEngine;

namespace Unity.Tutorial
{
    public class TutorialHandlerBehaviour : MonoBehaviour
    {
        [SerializeField] protected SO_Step _step;

        protected virtual void Awake() { }
        public void HandleInteraction() => _step.onInteracted?.Invoke();
    }
}