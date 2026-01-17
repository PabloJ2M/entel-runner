using Unity.Mathematics;
using UnityEngine;

namespace Unity.Pool
{
    public class Background3D : PoolObjectOnSpline
    {
        [SerializeField, Range(0f, 180f)] private float _maxAngle = 45f;

        private static readonly float3 _cameraPoint = new(0, 0, -10);
        private quaternion _initialRotation;

        private void Start() => _initialRotation = Transform.localRotation;

        protected override void UpdatePosition()
        {
            base.UpdatePosition();

            float3 localTarget = transform.InverseTransformPoint(_cameraPoint);
            float3 dir = math.normalize(new float3(-localTarget.x, 0f, localTarget.z));

            if (math.lengthsq(dir) < 0.0001f) return;

            float3 forward = new float3(0f, 0f, 1f);

            float3 cross = math.cross(forward, dir);
            float dot = math.dot(forward, dir);
            float angle = math.atan2(cross.y, dot);

            float clamped = math.clamp(angle, math.radians(-_maxAngle), math.radians(_maxAngle));

            quaternion targetRot = math.mul(_initialRotation, quaternion.AxisAngle(math.up(), clamped));
            quaternion current = transform.localRotation;

            transform.localRotation = math.nlerp(current, targetRot, 1 * Time.deltaTime);
        }
    }
}