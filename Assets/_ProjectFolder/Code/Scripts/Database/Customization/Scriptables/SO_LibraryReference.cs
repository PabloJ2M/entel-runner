//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [CreateAssetMenu(fileName = "SpriteLibraryReference", menuName = "system/customization/library reference")]
    public class SO_LibraryReference : ScriptableObject
    {
        [SerializeField] private string _libraryID;
        [SerializeField] private SpriteLibraryAsset _assetReference;
        [SerializeField] private RuntimeAnimatorController _animator;

        public string ID => _libraryID;
        public SpriteLibraryAsset Asset => _assetReference;
        public RuntimeAnimatorController Animator => _animator;

        //public IReadOnlyList<Sprite> GetSprites(string[] categories, string label)
        //{
        //    var list = new List<Sprite>();
            
        //    foreach (var category in categories)
        //        list.Add(_assetReference.GetSprite(category, label));

        //    return list;
        //}
    }
}