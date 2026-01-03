using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [RequireComponent(typeof(SpriteResolver))]
    public class SpriteResolverListener : MonoBehaviour
    {
        [SerializeField] private SpriteResolver _resolver;
        private SpriteRenderer _render;
        private string _categoryID;

        private void Awake()
        {
            _render = GetComponent<SpriteRenderer>();

            _categoryID = _resolver.GetCategory();
            GetComponentInParent<SpriteLibraryHandler>().AddListener(_categoryID, this);
        }
        private void Reset() => _resolver = GetComponent<SpriteResolver>();

        public void SetLabel(string label) => _render.enabled = _resolver.SetCategoryAndLabel(_categoryID, label);
    }
}