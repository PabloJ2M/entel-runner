namespace UnityEngine.UI
{
    public abstract class ToggleBehaviour : Toggle
    {
        protected override void OnEnable() { base.OnEnable(); onValueChanged.AddListener(OnUpdateValue); }
        protected override void OnDisable() { base.OnDisable(); onValueChanged.RemoveListener(OnUpdateValue); }
        protected abstract void OnUpdateValue(bool value);

        #if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            toggleTransition = ToggleTransition.None;
            graphic = null;
        }
        #endif
    }
}