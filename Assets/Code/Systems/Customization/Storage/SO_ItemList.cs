using System.Linq;
using UnityEngine;

namespace Unity.Customization
{
    public class SO_ItemList : ScriptableObject
    {
        [SerializeField] private SO_Item[] items;

        //public IEnumerable<SO_Item> GetItems(string[] categories)
        //{
        //    return items.Select(x => categories.Contains(x.Category));
        //}
    }
}