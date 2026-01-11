using System;

namespace Unity.Achievements
{
    public class AchievementEvent
    {
        public static Action<AchievementType, int> onAction { get; set; }
    }
}