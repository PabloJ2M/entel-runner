using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Customization
{
    [Serializable] public struct Sort
    {
        public ItemSortingGroup sortType;
        public int sortingOrder;
    }

    [CreateAssetMenu(fileName = "sorting group", menuName = "system/customization/sorting group")]
    public class SO_SortingGroup : ScriptableObject
    {
        [SerializeField] private Sort[] _sorts;

        public IReadOnlyList<Sort> Sorts => _sorts;
    }
}