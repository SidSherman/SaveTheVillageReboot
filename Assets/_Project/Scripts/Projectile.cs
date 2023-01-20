using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DamageDealler
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime = 5;

    public GameObject Owner
    {
        get => _owner;
        set => _owner = value;
    }

    private void Start()
    {
        StartCoroutine(WaitToDestroy(_lifeTime));
    }

    public void Push(int direction)
    {
        _rigidbody2D.velocity = new Vector2(direction*_speed, _rigidbody2D.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject != _owner)
        {
            if (!collision.isTrigger)
            {
                if (collision.gameObject.TryGetComponent(out HealthComponent healthComponent))
                {
                    healthComponent.TakeDamage(_damage);
                }
                StartCoroutine(WaitToDestroy(0));
            }
        }
    }

    private IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
