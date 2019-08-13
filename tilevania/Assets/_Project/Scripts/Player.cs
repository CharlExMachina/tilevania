using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody2D _rigidbodyComponent;
    private Animator _animatorComponent;
    private SpriteRenderer _spriteComponent;

    private const string HorizontalAxis = "Horizontal";

    // Animation keys
    private readonly int _running = Animator.StringToHash("Running");

    private void Awake()
    {
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
            var jumpVelocity = new Vector2(_rigidbodyComponent.velocity.x, jumpForce);
            _rigidbodyComponent.velocity = jumpVelocity;
        }
    }
}
