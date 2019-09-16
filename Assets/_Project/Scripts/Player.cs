using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody2D _rigidbodyComponent;
    private Animator _animatorComponent;
    private SpriteRenderer _spriteComponent;
    private Collider2D _colliderComponent;

    private int _deadLayer;
    private bool _isAlive;
    private const string HorizontalAxis = "Horizontal";

    // Animation keys
    private readonly int _running = Animator.StringToHash("Running");
    private readonly int _isAliveKey = Animator.StringToHash("IsAlive");

    private void Awake()
    {
        _colliderComponent = GetComponent<Collider2D>();
        _rigidbodyComponent = GetComponent<Rigidbody2D>();
        _animatorComponent = GetComponent<Animator>();
        _spriteComponent = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _isAlive = true;
        _deadLayer = 13;
    }

    private void Update()
    {
        if (_isAlive)
        {
            HandleHorizontalMovement();
            HandleJump();   
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Hazards"))
        {
            Die();
        }
    }

    private void Die()
    {
        _animatorComponent.SetBool(_isAliveKey, false);
        _isAlive = false;
        _rigidbodyComponent.velocity = Vector2.zero;
        PerformJump(9f);
        gameObject.layer = _deadLayer;
    }

    private void HandleHorizontalMovement()
    {
        var direction = Input.GetAxisRaw(HorizontalAxis);

        if (direction > 0f)
        {
            _spriteComponent.flipX = false;
        }
        else if (direction < 0f)
        { 
            _spriteComponent.flipX = true;
        }

        _rigidbodyComponent.velocity = new Vector2(direction * movementSpeed, _rigidbodyComponent.velocity.y);

        _animatorComponent.SetBool(_running, Math.Abs(direction) > 0.0f);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            // Shoot raycast downwards
            var hits = new RaycastHit2D[10];
            var filter = new ContactFilter2D
            {
                layerMask = LayerMask.GetMask("Ground", "Ladder"),
                useLayerMask = true,
                useTriggers = true
            };
        
            // get hits
            var numberOfHits = _colliderComponent.Cast(Vector2.down, filter, hits, 0.02f);

            // if the player hit the ground...
            if (numberOfHits > 0)
            {
                PerformJump(jumpForce);
            }
        }
    }

    private void PerformJump(float force)
    {
        var jumpVelocity = new Vector2(_rigidbodyComponent.velocity.x, force);
        _rigidbodyComponent.velocity = jumpVelocity;
    }
}
