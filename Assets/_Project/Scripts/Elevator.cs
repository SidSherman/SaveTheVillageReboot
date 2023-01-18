using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Elevator : InteractiveObject
{
    [SerializeField] private SliderJoint2D _slider;
    [SerializeField] private bool _shouldActivateAutomatically;
    [SerializeField] private GameObject[] _limits;
    public override void Activate()
    {
        base.Activate();
        JointMotor2D motor2D = new JointMotor2D();
        motor2D.motorSpeed = 1;
        motor2D.maxMotorTorque = 10000;
        _slider.motor = motor2D;
    }
    public override void Deactivate()
    {
        base.Deactivate();
        JointMotor2D motor2D = new JointMotor2D();
        motor2D.motorSpeed = -1;
        motor2D.maxMotorTorque = 10000;
        _slider.motor = motor2D;
    }

    public override void Use()
    {
        if (!_shouldActivateAutomatically)
        {
            base.Use();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_limits.Contains(collider.gameObject))
        {
            if (IsActive)
            {
            
                Deactivate();
            }
            else
            {

                Activate();
            }
        }
    }
}
