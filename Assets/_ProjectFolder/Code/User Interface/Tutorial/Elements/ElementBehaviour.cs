using UnityEngine;

namespace Unity.Tutorial.UI
{
    public abstract class ElementBehaviour : MonoBehaviour
    {
        protected TutorialManager _controller;
        protected bool _isEnabled;

        protected virtual void Awake() => _controller = TutorialManager.Instance;
        protected virtual void OnEnable()
        {
            _controller.OnStepStarted += OnStepStarted;
            _controller.OnStepCompleted += OnStepCompleted;
        }
        protected virtual void OnDisable()
        {
            _controller.OnStepStarted -= OnStepStarted;
            _controller.OnStepCompleted -= OnStepCompleted;
        }

        protected abstract void OnStepStarted(SO_Step step);
        protected abstract void OnStepCompleted();
    }
}