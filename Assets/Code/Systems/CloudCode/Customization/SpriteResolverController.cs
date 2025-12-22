using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [RequireComponent(typeof(SpriteLibrary))]
    public class SpriteResolverController : MonoBehaviour
    {
        private Dictionary<string, SpriteResolverElement> _resolvers = new();
        private SpriteLibrary _library;

        private const string _default = "default";

        public IEnumerable<string> Categories => _library.spriteLibraryAsset.GetCategoryNames();

        private void Awake() => _library = GetComponent<SpriteLibrary>();
        private void Start()
        {
            var categories = Categories;
            foreach (string category in categories)
                SetLabel(category, PlayerPrefs.GetString(category, _default));
        }

        public void AddListener(string category, SpriteResolverElement element) => _resolvers.Add(category, element);
        public void RemoveListener(string category) => _resolvers.Remove(category);

        public void SetLabel(string category, string label)
        {
            _resolvers[category].SetLabel(label);
            PlayerPrefs.SetString(category, label);
        }
    }
}