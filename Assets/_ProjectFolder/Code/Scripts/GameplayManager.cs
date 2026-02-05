using System;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : SingletonBasic<GameplayManager>
{
    [SerializeField] private AnimationCurve _acceleration;
    [SerializeField] private Vector2 _clampSpeed;
    [SerializeField] private float _time;
    [SerializeField] private bool _startDisabled;

    public Action<float> onDinstanceTraveled;
    public Action onFixedMovement;
    public UnityEvent onCompleteGame;

    private float _timeInv, _speedOnCurve;
    private float _currentSpeed;

    public float WorldDistance { get; private set; }
    public bool IsEnabled { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (_time != 0) _timeInv = 1f / _time;
        if (!_startDisabled) IsEnabled = true;
    }

    private void Update()
    {
        if (!IsEnabled || Time.timeScale == 0) return;

        WorldDistance += _currentSpeed * Time.deltaTime;
        onDinstanceTraveled?.Invoke(WorldDistance);

        if (_speedOnCurve >= 1f) return;

        _speedOnCurve = Mathf.Clamp01(_speedOnCurve + Time.deltaTime * _timeInv);
        _currentSpeed = Mathf.Lerp(_clampSpeed.x, _clampSpeed.y, _acceleration.Evaluate(_speedOnCurve));
    }
    private void LateUpdate()
    {
        if (!IsEnabled || Time.timeScale == 0) return;
        onFixedMovement?.Invoke();
    }

    public void Enable()
    {
        _currentSpeed = Mathf.Lerp(_clampSpeed.x, _clampSpeed.y, _acceleration.Evaluate(_speedOnCurve));
        IsEnabled = true;
    }
    public void Disable()
    {
        IsEnabled = false;
        _currentSpeed = 0f;
        onCompleteGame.Invoke();
    }
    public void ResetValues()
    {
        _speedOnCurve = 0f;
        Enable();
    }
}