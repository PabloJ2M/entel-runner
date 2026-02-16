using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    private float _inverseMaxHealth;
    private float _currentHealth;

    public event Action<float> onHealthUpdated;

    private void Awake() => _inverseMaxHealth = 1f / _maxHealth;
    private void Start() => ResetHealth();
    private void UpdateHealth() => onHealthUpdated?.Invoke(_currentHealth * _inverseMaxHealth);

    public void AddHealth(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
        UpdateHealth();
    }
    public void RemoveHealth(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - amount, 0, _maxHealth);
        UpdateHealth();
    }
    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        UpdateHealth();
    }
}