using Unity.Mathematics;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 _size;
    [SerializeField] private float _distance;

    private Transform _transform;

    private void Awake() => _transform = transform;
    private void FixedUpdate()
    {
        float threshold = _target.PositionY() - _transform.PositionY();
        _transform.localScale = math.lerp(_size.x, _size.y, threshold / _distance) * new float3(1, 1, 1);
    }
}