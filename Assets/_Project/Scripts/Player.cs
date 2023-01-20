using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    
    
    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _root;
    [SerializeField] private GameObject _shootingPoint;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private GameManager _gameManager;
    
    
    private PlayerAnimator _playerAnimatorScript;
    private SpriteRenderer _renderer;
    private GroundChecker _groundChecker;
    private HealthComponent _healthComponent;
    private PlayerMovementController _playerController;
    private Rigidbody2D _rigidbody;
    private PlayerControlls _input;
    private List<GameObject> _overlappedObjects;
    
    private bool _isFalling;
    private int _arrowCount;
    
    public HealthComponent HealthComponent => _healthComponent;
    public Rigidbody2D Rigidbody => _rigidbody;
    public bool IsFalling { get => _isFalling; set => _isFalling = value; }
    
    private Vector2 _inputDirection;
    public delegate void IntDelegate(int value);
    public event IntDelegate onArrowChange;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAnimatorScript = GetComponent<PlayerAnimator>();
        _renderer = GetComponent<SpriteRenderer>();
        _groundChecker = GetComponentInChildren<GroundChecker>();
        _healthComponent = GetComponent<HealthComponent>();
        _playerController = GetComponent<PlayerMovementController>();
        _gameManager = FindObjectOfType<GameManager>();
        
        _input = new PlayerControlls();
        _input.Player.Move.performed += MoveInput;
        _input.Player.Move.canceled += StopMove;
        _input.Player.Jump.started += Jump;
        _input.Player.Use.started += UseObjects;
        _input.Game.Pause.started += _gameManager.TogglePauseInput;
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
        _playerController.Move(_inputDirection);
        
        _isFalling = !_groundChecker.IsTouchTheGround;
        
        _playerAnimatorScript.SetGrounded(!_isFalling);
        _playerAnimatorScript.SetAirSpeedY(_rigidbody.velocity.y);
    }


    
    public void MoveInput(InputAction.CallbackContext context)
    {
     
        _inputDirection = context.ReadValue<Vector2>();
        
        if (_inputDirection.x > 0)
        {
            _sword.transform.localPosition = new Vector2(1, _sword.transform.localPosition.y);
            _renderer.flipX = false;
        }
        if (_inputDirection.x < 0)
        {
            _sword.transform.localPosition = new Vector2(-1, _sword.transform.localPosition.y);
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
        _playerController.Jump();
        _playerAnimatorScript.SetJump();
    }
    
    public void Death()
    {
       _playerAnimatorScript.SetDeath();
     
        if(TryGetComponent(out Collider2D collider2D))
        {
            collider2D.enabled = false;
        }
        DeactivateMovement();

        _gameManager.UnsetCameraFollowObject();
        
        _input.Disable();
        
        _gameManager.Lose();
        
        }
    
    public void ArrowChange(int value)
    {
        _arrowCount += value;
        onArrowChange(_arrowCount);
    }
    
    public void OnPush(Vector2 position, float force)
    {
        DeactivateMovement();
        _rigidbody.AddForce(_rigidbody.position - position * force, ForceMode2D.Impulse);
        if(!_healthComponent.IsDead)
            StartCoroutine(Stan());
    }
    public void DeactivateMovement()
    {
        _playerController.CanMove = false;
        
    }
    
    public void ActivateMovement()
    {
        _playerController.CanMove = true;
    }
    
    private void UseObjects(InputAction.CallbackContext context)
    {
        var objects = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        
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
    private void ActivateSword()
    {
        _sword.SetActive(true);
        _sword.GetComponent<DamageDealler>().ApplyDamage();
        
    }
    private void DeativateSword()
    {
        _sword.SetActive(false);
    }
    
    private void RangedAttacK(InputAction.CallbackContext context)
    {
        if (_arrowCount <= 0) return;

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
        
        ArrowChange(-1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("DynamicObject"))
        {
            var obj = collision.gameObject.GetComponent<DynamicObject>();
                transform.SetParent(obj.Root);
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DynamicObject"))
        {
            transform.SetParent(_root.transform);
        }
    }

    private IEnumerator Stan()
    {
        yield return new WaitForSeconds(0.5f);
        ActivateMovement();
    }
    
}
