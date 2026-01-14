using System;

namespace UnityEngine.UI
{
    public abstract class EnumToggleGroup<T> : ToggleGroup where T : Enum
    {
        [SerializeField] protected T _selected;

        #if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            if (Application.isPlaying)
                UpdateType(_selected);
        }
        #endif

        public abstract void UpdateType(T value);
        public virtual void CheckToggleStatus() { }
    }
}