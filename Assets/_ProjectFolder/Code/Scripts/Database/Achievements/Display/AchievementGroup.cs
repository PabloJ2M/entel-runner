using UnityEngine;
using UnityEngine.UI;

namespace Unity.Achievements.UI
{
    using Services.RemoteConfig;

    public class AchievementGroup : EnumToggleGroup<ConfigType>
    {
        [SerializeField] private AchievementUI _display;

        public override void UpdateType(ConfigType value)
        {
            if (value == _selected) return;

            _display.UpdateSelected(value);
            _selected = value;
        }
    }
}