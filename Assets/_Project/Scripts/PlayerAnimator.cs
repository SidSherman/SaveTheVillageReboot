using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimatorState (int state)
    {
        _animator.SetInteger("AnimState", state);
    }
    
    public void SetGrounded (bool value)
    {
        _animator.SetBool("Grounded", value);
    }
    
    public void SetAirSpeedY (float value)
    {
        _animator.SetFloat("AirSpeedY", value);
    }
    
    public void SetDeath ()
    {
        _animator.SetTrigger("Death");
    }
    
    public void SetHurt ()
    {
        _animator.SetTrigger("Hurt");
    }
    public void SetJump ()
    {
        _animator.SetTrigger("Jump");
    }
    public void SetAttack (int value)
    {
        if(value == 1)
            _animator.SetTrigger("Attack1");
        if(value == 2)
            _animator.SetTrigger("Attack2");
        if(value == 3)
            _animator.SetTrigger("Attack3");
    }
}
