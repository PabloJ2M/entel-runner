using PrimeTween;

namespace UnityEngine.Animations
{
    public class TweenRectPosition : TweenRectTransform
    {
        [SerializeField] protected Direction _direction;
        [SerializeField] protected Vector2 _overrideDistance;

        protected override void Awake()
        {
            base.Awake();
            Vector2 size = _transform.rect.size;
            Vector2 direction = _direction.Get();
            if (_overrideDistance.x != 0) size.x = _overrideDistance.x;
            if (_overrideDistance.y != 0) size.y = _overrideDistance.y;

            _from = _to = _transform.anchoredPosition;
            _to += new Vector3(direction.x * size.x, direction.y * size.y, 0f);
        }
        protected override void OnStart() { }
        protected override void OnComplete() => _tweenCore.onComplete?.Invoke();

        protected override void OnPerformePlay(bool value)
        {
            if (_tweenCore.IsEnabled == value) return;

            _tween = Tween.UIAnchoredPosition(_transform, _transform.anchoredPosition, value ? _from : _to, _settings);
            _tween.OnComplete(OnComplete);
        }
    }
}