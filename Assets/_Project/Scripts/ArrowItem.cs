using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowItem : InteractiveObject
{
 
    [SerializeField] private GameObject _particleSystem;

    private void Start()
    {

    }
    
    public override void Activate()
    {
        base.Activate();
        if(_particleSystem)
            Instantiate(_particleSystem, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_objectType == InteractiveObjectType.Overlapping)
        {
            if (other.gameObject.CompareTag("Player"))
            { 
               if(other.gameObject.TryGetComponent(out Player player))
               {
                   
                   player.ArrowChange(1);
                   
               }
               Activate();
            }
        }
    }
    

}