using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private bool _isTouchTheGround;
    [SerializeField] private string[] _layersName;
    
    public bool IsTouchTheGround
    {
        get => _isTouchTheGround;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_layersName.Contains(LayerMask.LayerToName(other.gameObject.layer)))
        {
            _isTouchTheGround = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_layersName.Contains(LayerMask.LayerToName(other.gameObject.layer)))
        {
            _isTouchTheGround = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            if (_layersName.Contains(LayerMask.LayerToName(other.gameObject.layer)))
            {
                _isTouchTheGround = true;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isTouchTheGround = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isTouchTheGround = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.layer);
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                _isTouchTheGround = true;
            }
        }
    }
}
