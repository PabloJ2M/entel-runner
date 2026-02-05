using PrimeTween;
using Unity.Mathematics;

namespace UnityEngine.Animations
{
    public class TweenRectScale : TweenRectTransform
    {
        [SerializeField] private Axis _axis = Axis.X | Axis.Y | Axis.Z;
        [SerializeField, Range(0f, 1f)] private float _normalFactor = 1f;
        [SerializeField, Range(0.5f, 1.5f)] private float _scaleFactor = 1f;

        protected override void Awake()
        {
            base.Awake();
            _from = _transform.localScale - _axis.Get() * (1f - _normalFactor);
            _to = _from + _axis.Get() * _scaleFactor;
            _transform.localScale = new(_from.x, _from.y, 1f);
        }

        protected override void OnStart() { }
        protected override void OnComplete() => _transform.localScale = !_tweenCore.IsEnabled ? _from : _to;
        protected override void OnPerformePlay(bool value)
        {
            if (_tweenCore.IsEnabled == value) return;
            if (_tween.isAlive) CancelTween();

            _tween = Tween.Scale(_transform, _transform.localScale, !value ? _from : _to, _settings);
            _tween.OnComplete(OnComplete);
        }
    }
}