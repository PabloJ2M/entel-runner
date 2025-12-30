using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [RequireComponent(typeof(SpriteResolver))]
    public class SpriteResolverElement : MonoBehaviour
    {
        private SpriteResolver _resolver;
        private SpriteRenderer _render;
        private string _categoryID;

        private void Awake()
        {
            _resolver = GetComponent<SpriteResolver>();
            _render = GetComponent<SpriteRenderer>();
            _categoryID = _resolver.GetCategory();

            GetComponentInParent<SpriteResolverController>().AddListener(_categoryID, this);
        }

        public void SetLabel(string label) => _render.enabled = _resolver.SetCategoryAndLabel(_categoryID, label);
    }
}