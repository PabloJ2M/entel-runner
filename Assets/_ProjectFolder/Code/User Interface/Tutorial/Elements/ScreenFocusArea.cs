using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Unity.Tutorial.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class ScreenFocusArea : ElementScreenBehaviour
    {
        [SerializeField] private Image _focusArea;
        [SerializeField] private UnityEvent<bool> _onVisibilityChanged;

        protected override void OnStepStarted(SO_Step step)
        {
            base.OnStepStarted(step);
            if (_target == null) return;

            _isEnabled = step.Type.HasFlag(TutorialType.ScreenFocusObject);
            if (!_isEnabled) return;

            _onVisibilityChanged.Invoke(true);
            _focusArea.SetSprite(_target.Shape);
            OnUpdateStatus();
        }
        protected override void OnStepCompleted()
        {
            if (!_isEnabled) return;

            _onVisibilityChanged.Invoke(false);
            _isEnabled = false;
        }
        protected override void OnUpdateStatus()
        {
            _focusArea.rectTransform.SetPosition(_target);
        }
    }
}