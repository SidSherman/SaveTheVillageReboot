using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : InteractiveObject
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _particleSystem;
    [SerializeField] private AudioClip _audioClip;

    private void Start()
    {
        if (!_gameManager)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
      
    }
    
    public override void Activate()
    {
        base.Activate();
        if(_gameManager)
            _gameManager.UpdateScore(1);
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
                Activate();
            }
        }
    }
    

}