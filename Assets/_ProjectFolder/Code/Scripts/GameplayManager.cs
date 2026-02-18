using System;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : SingletonBasic<GameplayManager>
{
    [SerializeField] private AnimationCurve _acceleration;
    [SerializeField] private Vector2 _clampSpeed;
    [SerializeField] private float _time;
    [SerializeField] private bool _startDisabled;

    public Action<double> onDinstanceTraveled { get; set; }
    public Action onFixedMovement;
    public UnityEvent onGameStarted, onGamePaused, onGameCompleted;

    private float _timeInv, _speedOnCurve;
    private float _currentSpeed;
    private bool _complete;

    public double WorldDistance { get; private set; }
    public bool IsEnabled { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (_time != 0) _timeInv = 1f / _time;
        if (!_startDisabled) Play();
    }

    private void Update()
    {
        if (_complete) return;

        WorldDistance += _currentSpeed * Time.deltaTime;
        onDinstanceTraveled?.Invoke(WorldDistance);

        if (!IsEnabled || _speedOnCurve >= 1f) return;

        _speedOnCurve = Mathf.Clamp01(_speedOnCurve + Time.deltaTime * _timeInv);
        _currentSpeed = Mathf.Lerp(_clampSpeed.x, _clampSpeed.y, _acceleration.Evaluate(_speedOnCurve));
    }
    private void LateUpdate()
    {
        if (_complete) return;

        onFixedMovement?.Invoke();
    }

    public void Play()
    {
        IsEnabled = true;
        onGameStarted.Invoke();
        _currentSpeed = Mathf.Lerp(_clampSpeed.x, _clampSpeed.y, _acceleration.Evaluate(_speedOnCurve));
    }
    public void Stop()
    {
        Pause();
        _complete = true;
        _currentSpeed = 0f;
        onGameCompleted.Invoke();
    }
    public void Pause()
    {
        IsEnabled = false;
        onGamePaused.Invoke();
    }

    public void ResetValues()
    {
        _speedOnCurve = 0f;
        _complete = false;
        Play();
    }
}