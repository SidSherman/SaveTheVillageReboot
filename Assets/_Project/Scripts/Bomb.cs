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
    
    private void Awake()
    {
        if (_bombRadius != 0)
        {
            _circleCollider2D.radius = _bombRadius;
        }

        if (_explosionForce != 0)
        {
            _pointEffector2D.forceMagnitude = _explosionForce;
        }

        Explode();
    }
    
    private void Explode()
    {
        StartCoroutine(WaitToDestroy(1));
    }

    private IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
