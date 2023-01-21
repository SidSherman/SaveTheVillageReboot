using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class ObjectsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private float _MaxYOffset;
    [SerializeField] private float _MaxXOffset;
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _spawnRate;
    [SerializeField] private bool _slouldSpawn = true;

    public bool SlouldSpawn
    {
        get => _slouldSpawn;
        set => _slouldSpawn = value;
    }

    private void Start()
    {
        if (_slouldSpawn)
            StartPawn();
    }

    private void StartPawn()
    {
        Spawn();
        StartCoroutine(StawnTimer(_spawnRate));
        StartCoroutine(StopSpawnTimer(_spawnTime));
    }

    private void Spawn()
    {
        float offsetX = Random.Range(- _MaxXOffset, _MaxXOffset);
        float offsetY = Random.Range(- _MaxYOffset, _MaxYOffset);

        Instantiate(_spawnObject, new Vector2(transform.position.x + offsetX, transform.position.y + offsetY),
            Quaternion.identity);
        if (_slouldSpawn)
            StartPawn();
    }
    private IEnumerator StawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Spawn();
        StartCoroutine(StawnTimer(_spawnTime));
    }
    
    private IEnumerator StopSpawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        _slouldSpawn = false;
    }

}
