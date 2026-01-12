using Unity.Mathematics;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float2 _limits = new(0.2f, 1.2f);
    [SerializeField] private float _distance = 10;

    private Transform _transform;
    private float3 _one = new(1f, 1f, 1f);
    private float _origin, _invDistance;

    private void Start()
    {
        _transform = transform;
        _origin = _transform.PositionY();
        _invDistance = _distance > 0f ? 1f / _distance : 0f;
    }
    private void FixedUpdate()
    {
        float distance = _target.PositionY() - _origin;
        float t = math.saturate(distance * _invDistance);

        _transform.localScale = math.lerp(_limits.x, _limits.y, t) * _one;
    }
}