using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealler : MonoBehaviour
{
    [SerializeField] protected float _damage;
    [SerializeField] protected float _force;
    [SerializeField] protected GameObject _owner;
    [SerializeField] protected bool _damageOnTouch = true;

    public void ApplyDamage()
    {
        var objects = Physics2D.OverlapBoxAll(transform.position,new Vector2(1,1),0);
               
        foreach (var activeObject in objects)
        {
            if (activeObject.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.TakeDamage(_damage);
                if (activeObject.gameObject.TryGetComponent(out Player player))
                {
                    player.OnPush(transform.position,_force);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_damageOnTouch)
            return;
        
        if (other.gameObject != _owner)
        {
            if (other.gameObject.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.TakeDamage(_damage);
                if (other.gameObject.TryGetComponent(out Player player))
                {
                    player.OnPush(transform.position,_force);
                }
            }
        }
     }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_damageOnTouch)
            return;
        
        if (other.gameObject != _owner)
        {
            if (other.gameObject.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.TakeDamage(_damage);
                if (other.gameObject.TryGetComponent(out Player player))
                {
                    player.OnPush(transform.position,_force);
                }
            }
        }
    }
}
