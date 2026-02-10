using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [RequireComponent(typeof(SpriteResolver))]
    public class SpriteResolverListener : MonoBehaviour
    {
        [SerializeField] private SpriteResolver _resolver;
        [SerializeField] private ItemGroup _group;

        private SpriteRenderer _render;
        private string _category;

        private void Reset() => _resolver = GetComponent<SpriteResolver>();
        private void Awake()
        {
            GetComponentInParent<SpriteLibraryHandler>().AddListener(_group, this);
            _render = GetComponent<SpriteRenderer>();
            _category = _resolver.GetCategory();
        }

        public void SetLabel(string label)
        {
            _render.enabled = _resolver.SetCategoryAndLabel(_category, label);
        }
    }
}