using UnityEngine;

namespace Unity.Pool
{
    using Mathematics;

    public class Background3D : PoolObjectOnSpline
    {
        [SerializeField] private Transform _render;
        [SerializeField, Range(0f, 180f)] private float _maxAngle = 45f;
        [SerializeField, Range(0f, 1f)] private float _angleFactor = 0.5f;
        [SerializeField] private bool _flip;

        private static readonly Vector3 _cameraPoint = new(0, 0, -10);
        private static readonly Vector3 _up = Vector3.up;
        private static readonly Vector3 _forward = Vector3.forward;

        private float _maxAngleRad;

        protected virtual void Awake() => _maxAngleRad = _maxAngle * Mathf.Deg2Rad;
        protected override void Reset()
        {
            base.Reset();
            _render = transform;
        }

        public override void RefreshPosition(float worldDistance)
        {
            base.RefreshPosition(worldDistance);

            Vector3 dir = _cameraPoint - _render.position;
            dir.y = 0f;

            dir = dir.normalized;
            if (_flip) dir = -dir;

            float dot = Vector3.Dot(_forward, dir);
            float sign = Mathf.Sign(math.cross(_forward, dir).y);

            float normalized = 1f - dot;

            float scaled = normalized * _angleFactor;
            float finalAngle = scaled * _maxAngleRad;

            quaternion rot = quaternion.AxisAngle(_up, finalAngle * sign);
            _render.localRotation = !_flip ? rot : math.inverse(rot);
        }
    }
}