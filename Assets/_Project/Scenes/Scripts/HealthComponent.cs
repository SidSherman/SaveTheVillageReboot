
using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private GameObject _owner;
    [SerializeField] private int _team;
    
    private bool _isDead = false;
    private float _currentHealth;
    
    public bool IsDead => _isDead;
    public int Team => _team;

    public delegate void VoidDelegate();
    public delegate void FloatDelegate(float value);
    
    public event VoidDelegate onDeath;
    public event FloatDelegate onHealthChange;
    
    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    
    public void Death()
    {
        _isDead = true;
        onDeath();
    }

    public void TakeDamage(float damage)
    {
        if (_isDead)
            return;
        
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Death();
        }

        onHealthChange(_currentHealth);
      
    }
}
