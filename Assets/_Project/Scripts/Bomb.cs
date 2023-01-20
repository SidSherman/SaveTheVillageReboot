using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _bombRadius = 0;
    [SerializeField] private float _explosionForce = 0;
    [SerializeField] private CircleCollider2D _circleCollider2D;
    [SerializeField] private PointEffector2D _pointEffector2D;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _damage;
    [SerializeField] private DamageDealler _damageDealler;
    private void Start()
    {
         _circleCollider2D = GetComponent<CircleCollider2D>();
         _pointEffector2D = GetComponent<PointEffector2D>();
         _healthComponent = GetComponent<HealthComponent>();
         _renderer = GetComponent<SpriteRenderer>();
        
        _healthComponent.onDeath += Explode;
        _healthComponent.onHealthChange += Nothing;
    }

    private void Explode()
    {
        _circleCollider2D.radius = _bombRadius;
        _pointEffector2D.forceMagnitude = _explosionForce;
        _renderer.enabled = false;
   
        StartCoroutine(WaitToDestroy(0.3f));
        
        var objects = Physics2D.OverlapCircleAll(transform.position, _bombRadius);
        
        foreach (var activeObject in objects)
        {
            if (activeObject.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.TakeDamage(_damage);
            }
        }
    }
    private void Nothing(float value)
    {
      
    }

    private IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
