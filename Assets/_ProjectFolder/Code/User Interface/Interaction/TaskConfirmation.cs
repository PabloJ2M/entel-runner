using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class TaskConfirmation : MonoBehaviour
{
    [SerializeField] private TweenGroup _animation;

    private WaitUntil _waitForContinue;
    private Action _action;

    private void Awake() => _waitForContinue = new(() => _action == null);

    public IEnumerator DisplayTask(Action action)
    {
        _action = action;
        _animation.EnableTween();
        yield return _waitForContinue;
    }

    public void Interact()
    {
        _action.Invoke();
        Close();
    }
    public void Close()
    {
        _animation.DisableTween();
        _action = null;
    }
}