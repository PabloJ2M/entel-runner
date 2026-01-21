using System.Collections;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.Animations;

namespace Unity.Pool
{
    using Mathematics;

    public abstract class PoolObjectOnSpline : PoolObjectBehaviour
    {
        [Header("Position Handler")]
        [SerializeField] private Axis _snap;
        [SerializeField] private bool _roundPosition;

        protected float _currentTime, _splineLength;
        protected SplineContainer _spline;

        public SplineContainer Spline {
            set {
                _spline = value;
                _splineLength = 1f / _spline.CalculateLength();
            }
        }

        public override void Enable()
        {
            base.Enable();
            _currentTime = 0;
        }
        protected virtual void UpdatePosition()
        {
            float3 position = _spline.EvaluatePosition(_currentTime);
            if ((_snap & Axis.X) != 0) Transform.PositionX(_roundPosition ? math.round(position.x) : position.x);
            if ((_snap & Axis.Y) != 0) Transform.PositionY(_roundPosition ? math.round(position.y) : position.y);

            if (_currentTime >= 1f) StartCoroutine(Release());
        }

        private IEnumerator Release()
        {
            yield return null;
            Destroy();
        }

        public void SetTime(float value)
        {
            _currentTime = value;
            UpdatePosition();
        }
        public void SetDistance(float value)
        {
            _currentTime = value * _splineLength;
            UpdatePosition();
        }

        public virtual void AddTime(float amount)
        {
            _currentTime += amount;
            UpdatePosition();
        }
        public virtual void AddDistance(float amount)
        {
            _currentTime += amount * _splineLength;
            UpdatePosition();
        }
    }
}