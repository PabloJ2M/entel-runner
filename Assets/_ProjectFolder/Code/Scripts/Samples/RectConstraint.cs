using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RectConstraint : MonoBehaviour
{
    [SerializeField] private RectTransform _source;
    private RectTransform _transform;

    private void Awake() => _transform = transform as RectTransform;
    private void LateUpdate() => _transform.sizeDelta = _source.sizeDelta;
}