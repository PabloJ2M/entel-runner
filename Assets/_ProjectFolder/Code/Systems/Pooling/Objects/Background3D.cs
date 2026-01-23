using UnityEngine;

namespace Unity.Pool
{
    using Mathematics;

    public class Background3D : PoolObjectOnSpline
    {
        [SerializeField, Range(0f, 180f)] private float _maxAngle = 45f;
        [SerializeField, Range(0f, 1f)] private float _angleFactor = 0.5f;
        [SerializeField] private bool _flip;

        private static readonly float3 _cameraPoint = new(0, 0, -10);
        private static readonly float3 _forward = math.forward();
        private float _maxAngleRad;

        private void Awake() => _maxAngleRad = math.radians(_maxAngle);

        protected override void RefreshPosition()
        {
            base.RefreshPosition();

            float3 dir = _cameraPoint - (float3)Transform.localPosition;
            dir.y = 0f;

            dir = math.normalize(dir);
            if (_flip) dir = -dir;

            float dot = math.dot(_forward, dir);
            float sign = math.sign(math.cross(_forward, dir).y);

            float normalized = (1f - dot) * 0.5f;

            float scaled = normalized * _angleFactor;
            float finalAngle = math.min(scaled * math.PI, _maxAngleRad);

            quaternion rot = quaternion.AxisAngle(math.up(), finalAngle * sign);
            Transform.localRotation = !_flip ? rot : math.inverse(rot);
        }
    }
}