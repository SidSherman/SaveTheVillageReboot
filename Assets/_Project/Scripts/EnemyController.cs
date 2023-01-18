using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _goundSpeed;
   [SerializeField] private float _jumpHeight;

    [Header("Components")] 
    [SerializeField] private PlayerAnimator _AnimatorScript;
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _deathVFX;
    [SerializeField] private GameObject _root;
    [SerializeField] private HealthComponent _healthComponent;

    public HealthComponent HealthComponent => _healthComponent;

    [SerializeField] private GameObject _shootingPoint;
    [SerializeField] private GameObject _arrow;
    
    private List<GameObject> _overlappedObjects;
    
    private Rigidbody2D _rigidbody;

    private bool _canMove = true;
    private bool _isFalling;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
