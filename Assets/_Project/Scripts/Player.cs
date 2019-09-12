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

    private const string HorizontalAxis = "Horizontal";

    // Animation keys
    private readonly int _running = Animator.StringToHash("Running");

    private void Awake()
    {
        _colliderComponent = GetComponent<Collider2D>();
        _rigidbodyComponent = GetComponent<Rigidbody2D>();
        _animatorComponent = GetComponent<Animator>();
        _spriteComponent = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleHorizontalMovement();
        HandleJump();
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
                layerMask = LayerMask.GetMask("Ground"),
                useLayerMask = true
            };
        
            // get hits
            var numberOfHits = _colliderComponent.Cast(Vector2.down, filter, hits, 0.02f);

            // if the player hit the ground...
            if (numberOfHits > 0)
            {
                Debug.Log("Can jump!");
                var jumpVelocity = new Vector2(_rigidbodyComponent.velocity.x, jumpForce);
                _rigidbodyComponent.velocity = jumpVelocity;
            }
        }
    }
}
