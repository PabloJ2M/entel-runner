using System;

namespace UnityEngine.UI
{
    public abstract class EnumToggleTab<T> : MonoBehaviour where T : Enum
    {
        [SerializeField] protected T _value;
        protected EnumToggleGroup<T> _group;

        protected virtual void Awake() => GetComponent<Toggle>().onValueChanged.AddListener(OnClick);
        protected virtual void Start() => _group = GetComponentInParent<EnumToggleGroup<T>>();

        protected abstract void OnClick(bool isOn);
    }
}