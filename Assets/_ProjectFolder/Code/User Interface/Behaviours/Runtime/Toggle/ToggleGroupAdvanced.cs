namespace UnityEngine.UI
{
    public class ToggleGroupAdvanced : ToggleGroup
    {
        public void CloseAllToggles(bool value)
        {
            if (value) return;
            SetAllTogglesOff(false);
        }
    }
}