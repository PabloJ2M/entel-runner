using UnityEngine;

public class UnlockedCharacter : MonoBehaviour
{
    [SerializeField] private Material _normalMaterial, _lockedMaterial;

    private SpriteRenderer[] _renderers;

    private void Awake() => _renderers = GetComponentsInChildren<SpriteRenderer>();
    public void HasPurchased(bool value) => SetMaterials(value ? _normalMaterial : _lockedMaterial);

    private void SetMaterials(Material material)
    {
        foreach (var renderer in _renderers)
            renderer.material = material;
    }
}