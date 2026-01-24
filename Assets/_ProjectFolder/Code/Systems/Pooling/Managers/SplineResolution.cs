using UnityEngine;
using UnityEngine.Splines;

namespace Unity.Pool
{
    using Mathematics;

    public interface ISplineResolution
    {
        float LengthInv { get; }
        float3 GetPosition(float t);
    }

    [DefaultExecutionOrder(-100), RequireComponent(typeof(SplineContainer))]
    public class SplineResolution : MonoBehaviour, ISplineResolution
    {
        [Tooltip("number of points in spline")]
        [SerializeField, Range(2, byte.MaxValue)] private byte _resolution = 2;

        private float3[] _cachePoints;
        private float _splineLengthInv;

        public float LengthInv => _splineLengthInv;

        private void Awake()
        {
            var spline = GetComponent<SplineContainer>();

            _cachePoints = new float3[_resolution];
            _splineLengthInv = 1f / spline.CalculateLength();

            for (int i = 0; i < _resolution; i++)
            {
                float t = i / (float)(_resolution - 1);
                _cachePoints[i] = spline.EvaluatePosition(t);
            }
        }

        public float3 GetPosition(float t)
        {
            float f = math.clamp(t, 0f, 1f) * (_resolution - 1);
            int a = (int)math.floor(f);
            int b = math.min(a + 1, _resolution - 1);

            float lerp = f - a;
            return math.lerp(_cachePoints[a], _cachePoints[b], lerp);
        }
    }
}