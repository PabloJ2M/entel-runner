using UnityEngine;

namespace Unity.Pool
{
    using Mathematics;

    public abstract class PoolObjectOnSpline : PoolObjectBehaviourTransform
    {
        protected float _startDistance;
        private float3 _lastPosition, _currentPosition;
        private float _lastDeltaTime;

        public ISplineResolution Spline { get; set; }

        public override void Enable()
        {
            Transform.position = _lastPosition = _currentPosition = Spline.GetPosition(0);
            base.Enable();
        }

        public virtual void RefreshPosition(float worldDistance)
        {
            float t = (worldDistance - _startDistance) * Spline.LengthInv;
            if (t >= 1f) { Destroy(); return; }

            float3 position = Spline.GetPosition(t);
            _lastPosition = _currentPosition;
            _lastDeltaTime = Time.deltaTime;

            _currentPosition = position;
        }
        public virtual void FixedInterpolation()
        {
            if (_lastDeltaTime <= 0) return;
            Transform.position = Vector3.Lerp(_lastPosition, _currentPosition, Time.deltaTime / _lastDeltaTime);
        }

        public void SetDistance(float value) => _startDistance = value;
    }
}