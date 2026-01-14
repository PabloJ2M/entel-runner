using UnityEngine.UI;

namespace Unity.Achievements.UI
{
    using Services.RemoteConfig;

    public class AchievementTab : EnumToggleTab<ConfigType>
    {
        protected override void OnClick(bool isOn)
        {
            if (isOn)
                _group?.UpdateType(_value);
        }
    }
}