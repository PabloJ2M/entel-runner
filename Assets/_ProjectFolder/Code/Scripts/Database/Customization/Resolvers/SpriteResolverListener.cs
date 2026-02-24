using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [RequireComponent(typeof(SpriteResolver))]
    public class SpriteResolverListener : MonoBehaviour
    {
        [SerializeField] private SpriteResolver _resolver;
        [SerializeField] private ItemSortingGroup _sort;
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
            _resolver.SetCategoryAndLabel(_category, label);
        }
        public void SetSortingOrder(IReadOnlyList<Sort> list)
        {
            var value = list.FirstOrDefault(x => x.sortType == _sort);
            _render.sortingOrder = value.sortingOrder;
        }
    }
}