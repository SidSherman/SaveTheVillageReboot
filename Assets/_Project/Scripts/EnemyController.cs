using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _goundSpeed;
    [SerializeField] private float _waitTime;

    [Header("Components")] 
    [SerializeField] private PlayerAnimator _AnimatorScript;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _root;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private int state = 1;
    [SerializeField] private static GameManager _gameManager;
    public HealthComponent HealthComponent => _healthComponent;

    [SerializeField] private GameObject _shootingPoint;
    [SerializeField] private GameObject _arrow;
    
    private List<GameObject> _overlappedObjects;
    
    private Rigidbody2D _rigidbody;

    private bool _canMove = true;
    private bool _isFalling;
    
    private int MOVE_RIGHT_STATE = 2;
    private int MOVE_LEFT_STATE = 3;

    private void Start()
    {
    
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _healthComponent.onDeath += Death;
        _healthComponent.onHealthChange += Nothing;
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Move();
        }
    }

    public void Death()
    {
        if(TryGetComponent(out Collider collider))
        {
            collider.enabled = false;
        }

        _rigidbody.simulated = false;
        
        _AnimatorScript.SetDeath();
        _canMove = false;
        GameManager.EnemyKilled = GameManager.EnemyKilled + 1;
        //_gameManager.EnemyKilled++;
    }
    private void Nothing(float value)
    {
      
    }
    
    public void Move()
    {
        
        float speed = 0;

        if (state == MOVE_LEFT_STATE)
        {
            _renderer.flipX = true;
            speed = _goundSpeed * -1;
        }

        if (state == MOVE_RIGHT_STATE)
        {
            _renderer.flipX = false;
            speed = _goundSpeed * 1;
        }
       
        _AnimatorScript.SetAnimatorState(1);
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Wall wall))
        {
            StartCoroutine(Wait(_waitTime, state));
        }
  }

    private IEnumerator Wait(float time, int currentDirection)
    {
        _canMove = false;
        _AnimatorScript.SetAnimatorState(0);
        yield return new WaitForSeconds(time);

        if (currentDirection == MOVE_LEFT_STATE)
        {
            state = MOVE_RIGHT_STATE;
        }
        if (currentDirection == MOVE_RIGHT_STATE)
        {
            state = MOVE_LEFT_STATE;
        }

        _canMove = true;
    }
}



