using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Achievements
{
    using Services.RemoteConfig;

    public class AchievementField : MonoBehaviour
    {
        [SerializeField] private AchievementController _controller;
        [SerializeField] private SO_Achievement_List _reference;
        [SerializeField] private ConfigType _type;

        private void Reset() => _controller = GetComponentInParent<AchievementController>();
        private void Awake() => _controller?.AddListener(this, _type);
        private void OnDestroy() => _controller?.RemoveListener(_type);

        public IReadOnlyCollection<SO_Achievement> Parse(string[] ids)
        {
            var items = new List<SO_Achievement>();
            foreach (var id in ids) {
                if (TryGet(id, out var item))
                    items.Add(item);
            }
            return items;
        }
        public void ResetAchievements(string previusUpdate, string currentUpdate)
        {
            DateTime.TryParse(previusUpdate, out DateTime previusDate);
            DateTime.TryParse(currentUpdate, out DateTime currentDate);

            if (currentDate > previusDate)
                _reference.ResetAchievements();
        }

        private bool TryGet(string id, out SO_Achievement achievement)
        {
            achievement = _reference.Get(id);
            return achievement != null;
        }
    }
}