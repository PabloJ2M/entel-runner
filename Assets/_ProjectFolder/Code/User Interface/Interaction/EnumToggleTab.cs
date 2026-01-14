using System;

namespace UnityEngine.UI
{
    public abstract class EnumToggleTab<T> : MonoBehaviour where T : Enum
    {
        [SerializeField] protected T _value;
        protected EnumToggleGroup<T> _group;

        protected virtual void Awake()
        {
            _group = GetComponentInParent<EnumToggleGroup<T>>();
            GetComponent<Toggle>().onValueChanged.AddListener(OnClick);
        }

        protected abstract void OnClick(bool isOn);
    }
}