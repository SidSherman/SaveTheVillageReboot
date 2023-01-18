
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [FormerlySerializedAs("_goundAcceleration")] [SerializeField] private float _goundSpeed;
    [FormerlySerializedAs("_airAcceleration")] [SerializeField] private float _airSpeed;
    [SerializeField] private float _jumpHeight;

    [Header("Components")] 
    [SerializeField] private PlayerAnimator _playerAnimatorScript;
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _deathVFX;
    [SerializeField] private GameObject _root;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private HealthComponent _healthComponent;

    public HealthComponent HealthComponent => _healthComponent;

    [SerializeField] private GameObject _shootingPoint;
    [SerializeField] private GameObject _arrow;
    
    private List<GameObject> _overlappedObjects;
    
    private Rigidbody2D _rigidbody;
    private PlayerControlls _input;

    private bool _canMove = true;
    private bool _isFalling;
    
    private Vector2 _inputDirection;
   

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _input = new PlayerControlls();
        _input.Player.Move.performed += MoveInput;
        _input.Player.Move.canceled += StopMove;
        _input.Player.Jump.started += Jump;
        _input.Player.Use.started += UseObjects;

        _input.Player.MeleeAttack.started += MeleeAttacK;
        _input.Player.RangedAttack.started += RangedAttacK;
        
        _healthComponent.onDeath += Death;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
    
    private void FixedUpdate()
    {
        if (_canMove)
        {
            Move();
        }
        _isFalling = !_groundChecker.IsTouchTheGround;
        _playerAnimatorScript.SetGrounded(!_isFalling);
        _playerAnimatorScript.SetAirSpeedY(_rigidbody.velocity.y);
    }

    public void Move()
    {
        var speed = _isFalling ? _airSpeed : _goundSpeed;
       
        _rigidbody.velocity = new Vector2(_movementCurve.Evaluate(_inputDirection.x), _rigidbody.velocity.y);
    }
    
    public void MoveInput(InputAction.CallbackContext context)
    { 
        _inputDirection = context.ReadValue<Vector2>();
        if (_inputDirection.x > 0)
        {
            _renderer.flipX = false;
        }
        if (_inputDirection.x < 0)
        {
            _renderer.flipX = true;
        }
        _playerAnimatorScript.SetAnimatorState(1);
    }
    
    public void StopMove(InputAction.CallbackContext context)
    {
        _inputDirection = Vector2.zero;
        _playerAnimatorScript.SetAnimatorState(0);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!_isFalling)
        {
            _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode2D.Impulse);
        }
        _playerAnimatorScript.SetJump();
    }
    
    public void Death()
    {
       
        if(TryGetComponent(out MeshRenderer renderer))
        {
            renderer.enabled = false;
        }

        if(TryGetComponent(out Collider collider))
        {
            collider.enabled = false;
        }
        DeactivateMovement();
        Instantiate(_deathVFX, transform.position, transform.rotation);
    }
    
    public void DeactivateMovement()
    {
        _canMove = false;
        
        if(_rigidbody)
        {
            _rigidbody.isKinematic = true;
        }
    }
    
    public void ActivateMovement()
    {
        _canMove = true;
        
        if(_rigidbody)
        {
            _rigidbody.isKinematic = false;
        }
    }
    
    private void UseObjects(InputAction.CallbackContext context)
    {
        var objects = Physics.OverlapSphere(transform.position, 0.5f);
        
        foreach (var activeObject in objects)
        {
            if (activeObject.TryGetComponent(out InteractiveObject obj))
            {
                if (obj.ObjectType == InteractiveObject.InteractiveObjectType.Usable)
                {
                    obj.Use();
                }
            }
                
        }
    }

    private void MeleeAttacK(InputAction.CallbackContext context)
    {
        int i = Random.Range(1,4);
        _playerAnimatorScript.SetAttack(i);
    }
    
    private void RangedAttacK(InputAction.CallbackContext context)
    {
        Quaternion arrowRotatin;
        int direction;
        if (_renderer.flipX )
        {
            direction = -1;
            arrowRotatin = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            direction = 1;
            arrowRotatin = Quaternion.Euler(0, 0, 0);
        }
        
        GameObject arrow = Instantiate(_arrow, _shootingPoint.transform.position, arrowRotatin);
        arrow.GetComponent<Projectile>().Owner = gameObject;
        arrow.GetComponent<Projectile>().Push(direction);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        if (collision.gameObject.CompareTag("DynamicObject"))
        {
            Debug.Log("DynamicObject");
            var obj = collision.gameObject.GetComponent<DynamicObject>();
                transform.SetParent(obj.Root);
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("DynamicObject"))
        {
            transform.SetParent(_root.transform);
        }
    }
}
