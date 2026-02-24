using System;
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
        public Action<bool> onStatusChanged;

        public override void Enable()
        {
            if (Spline == null) return;
            Transform.position = _lastPosition = _currentPosition = Spline.GetPosition(0);
            onStatusChanged?.Invoke(true);
            base.Enable();
        }
        public override void Disable()
        {
            onStatusChanged?.Invoke(false);
            base.Disable();
        }

        public virtual void SetDistance(float value) => _startDistance = value;
        public virtual void RefreshPosition(float worldDistance)
        {
            float t = ((worldDistance - _startDistance) /*% Spline.Length*/) * Spline.LengthInv;
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
    }
}