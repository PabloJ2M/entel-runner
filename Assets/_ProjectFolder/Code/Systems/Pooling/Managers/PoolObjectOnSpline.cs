using UnityEngine;
using UnityEngine.Animations;

namespace Unity.Pool
{
    using Mathematics;

    public abstract class PoolObjectOnSpline : PoolObjectBehaviourTransform
    {
        [Header("Position Handler")]
        [SerializeField] private Axis _followAxis;

        protected float _startDistance;
        private float3 _lastPosition, _currentPosition;
        private float _lastDeltaTime;

        public ISplineResolution Spline { protected get; set; }

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

            if ((_followAxis & Axis.X) != 0) _currentPosition.x = position.x;
            if ((_followAxis & Axis.Y) != 0) _currentPosition.y = position.y;
        }
        public virtual void FixedInterpolation()
        {
            Transform.position = Vector3.Lerp(_lastPosition, _currentPosition, Time.deltaTime / _lastDeltaTime);
        }

        public void SetDistance(float value) => _startDistance = value;
    }
}