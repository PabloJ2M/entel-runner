using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Customization
{
    [CreateAssetMenu(fileName = "list", menuName = "storage/list", order = 0)]
    public class SO_ItemList : ScriptableObject
    {
        [SerializeField] private SO_Item[] _items;

        public IEnumerable<SO_Item> GetItems(IEnumerable<string> category)
        {
            if (_items == null || category == null) yield break;
            var categorySet = new HashSet<string>(category);

            foreach (var item in _items)
            {
                if (item == null) continue;

                if (categorySet.Contains(item.Category))
                    yield return item;
            }
        }
    }
}