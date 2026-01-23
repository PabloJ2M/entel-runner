using UnityEngine;
using UnityEngine.Animations;

namespace Unity.Pool
{
    using Mathematics;

    public abstract class PoolObjectOnSpline : PoolObjectBehaviour
    {
        [Header("Position Handler")]
        [SerializeField] private Axis _followAxis;
        [SerializeField] private bool _roundPosition;

        protected float _currentTime;

        public ISplineResolution Spline { protected get; set; }

        public override void Enable()
        {
            base.Enable();
            _currentTime = 0;
        }
        protected virtual void RefreshPosition()
        {
            if (_currentTime >= 1f) {
                Destroy();
                return;
            }

            float3 position = Spline.GetPosition(_currentTime);
            if ((_followAxis & Axis.X) != 0) Transform.PositionX(_roundPosition ? math.round(position.x) : position.x);
            if ((_followAxis & Axis.Y) != 0) Transform.PositionY(_roundPosition ? math.round(position.y) : position.y);
        }

        public void SetTime(float time)
        {
            _currentTime = time;
            RefreshPosition();
        }
        public void SetDistance(float value)
        {
            _currentTime = value * Spline.LengthInv;
            RefreshPosition();
        }

        public virtual void AddTime(float amount)
        {
            _currentTime += amount;
            RefreshPosition();
        }
        public virtual void AddDistance(float amount)
        {
            _currentTime += amount * Spline.LengthInv;
            RefreshPosition();
        }
    }
}