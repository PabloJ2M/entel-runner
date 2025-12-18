using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(SpriteResolver))]
public class SpriteResolverElement : MonoBehaviour
{
    private SpriteResolver _resolver;
    private SpriteResolverController _controller;

    public string Category => _resolver.GetCategory();

    private void Awake()
    {
        _resolver = GetComponent<SpriteResolver>();
        _controller = GetComponentInParent<SpriteResolverController>();
    }
    private void OnEnable() => _controller.AddListener(Category, this);
    private void OnDisable() => _controller.RemoveListener(Category);

    public void SetLabel(string label) => _resolver.SetCategoryAndLabel(Category, label);
}