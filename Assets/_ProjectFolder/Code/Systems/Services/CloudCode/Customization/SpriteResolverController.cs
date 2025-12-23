using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    using Services;
    using Services.CloudSave;

    [RequireComponent(typeof(SpriteLibrary))]
    public class SpriteResolverController : MonoBehaviour
    {
        [SerializeField] private SO_ItemList _listReference;

        private Dictionary<string, SpriteResolverElement> _resolvers = new();
        private PlayerDataService _player;

        public IEnumerable<string> Categories => _listReference.Library.GetCategoryNames();

        private void Awake() => _player = UnityServiceInit.Instance.GetComponent<PlayerDataService>();
        private void OnEnable() => _player.onDataUpdated += Start;
        private void OnDisable() => _player.onDataUpdated -= Start;

        private void Start()
        {
            var categories = Categories;
            foreach (var equipped in _player.Customization.equipped)
                SetLabel(equipped.Key, _listReference.GetItemByID(equipped.Key, equipped.Value).Label);
        }

        public void AddListener(string category, SpriteResolverElement element) => _resolvers.Add(category, element);
        public void RemoveListener(string category) => _resolvers.Remove(category);
        public void SetLabel(string category, string label) => _resolvers[category].SetLabel(label);
    }
}