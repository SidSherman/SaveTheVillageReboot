using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [Header("Movement")]
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private AnimationCurve _airMovementCurve;
    [SerializeField] private float _jumpHeight;
    private bool _canMove = true;

    public bool CanMove
    {
        get => _canMove;
        set => _canMove = value;
    }

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = _player.Rigidbody;
    }
    

    public void Move( Vector2 inputDirection)
    {
        if (_canMove)
        {
            if (_player.IsFalling)
            {
                _rigidbody.velocity = new Vector2(_airMovementCurve.Evaluate(inputDirection.x), _rigidbody.velocity.y);
            }
            else
            {
                _rigidbody.velocity = new Vector2(_movementCurve.Evaluate(inputDirection.x), _rigidbody.velocity.y);
            }
        }
      
    }
    
    public void Jump()
    {
        if (!_player.IsFalling)
        {
            _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode2D.Impulse);
        }
    }
    
}
