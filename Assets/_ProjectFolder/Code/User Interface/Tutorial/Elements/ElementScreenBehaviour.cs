namespace Unity.Tutorial.UI
{
    public abstract class ElementScreenBehaviour : ElementBehaviour
    {
        protected IElement _target;

        protected virtual void LateUpdate()
        {
            if (_isEnabled)
                OnUpdateStatus();
        }

        protected override void OnStepStarted(SO_Step step) => _target = step.Element;
        protected abstract void OnUpdateStatus();
    }
}