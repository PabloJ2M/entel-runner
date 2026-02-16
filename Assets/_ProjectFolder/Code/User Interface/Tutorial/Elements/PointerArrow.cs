using UnityEngine;
using Unity.Mathematics;

namespace Unity.Tutorial.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class PointerArrow : ElementScreenBehaviour
    {
        [SerializeField] private RectTransform _arrow;
        [SerializeField, Range(0f, 365f)] private float _angle;

        private RectTransform _canvas;

        protected override void Awake()
        {
            base.Awake();
            _arrow.gameObject.SetActive(false);
            _canvas = GetComponentInParent<Canvas>().transform as RectTransform;
        }
        protected override void OnStepStarted(SO_Step step)
        {
            base.OnStepStarted(step);
            if (_target == null) return;

            _isEnabled = step.Type.HasFlag(TutorialType.DisplayArrow);
            if (!_isEnabled) return;

            _arrow.gameObject.SetActive(true);
            OnUpdateStatus();
        }
        protected override void OnStepCompleted()
        {
            if (!_isEnabled) return;

            _arrow.gameObject.SetActive(false);
            _isEnabled = false;
        }
        protected override void OnUpdateStatus()
        {
            bool h = _target.Position.x < _canvas.position.x;
            bool v = _target.Position.y < _canvas.position.y;

            _arrow.rotation = quaternion.Euler(v ? 180f : 0f, h ? 180f : 0f, -_angle);

            _arrow.localPosition = new(
                h ? _target.Rect.xMax : _target.Rect.xMin,
                v ? _target.Rect.yMax : _target.Rect.yMin);
        }
    }
}