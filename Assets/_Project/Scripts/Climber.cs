﻿using System.Collections;
using UnityEngine;

public class Climber : MonoBehaviour
{
    [SerializeField] private float climbingSpeed;
    [SerializeField] private float jumpCooldown;

    private const string LadderLayer = "Ladder";

    private readonly int _climbing = Animator.StringToHash("Climbing");

    private Collider2D _colliderComponent;
    private Rigidbody2D _rigidbodyComponent;
    private Animator _animatorComponent;

    private bool _canClimb;
    private bool _jumped;

    private void Start()
    {
        _jumped = false;
        _animatorComponent = GetComponent<Animator>();
        _rigidbodyComponent = GetComponent<Rigidbody2D>();
        _colliderComponent = GetComponent<Collider2D>();
    }

    private void Update()
    {
        HandleClimbing();
    }

    private void HandleClimbing()
    {
        if (_canClimb)
        {
            var direction = Input.GetAxisRaw("Vertical");

            if (direction < 0 || direction > 0)
            {
                _animatorComponent.speed = 1;
                _animatorComponent.SetBool(_climbing, true);
            }
            else
            {
                _animatorComponent.speed = 0;
            }

            _rigidbodyComponent.gravityScale = 0f;
            _rigidbodyComponent.velocity = new Vector2(_rigidbodyComponent.velocity.x, direction * climbingSpeed);

            if (Input.GetButton("Jump") && !_jumped)
            {
                _jumped = true;
                StopClimbing();
                StartCoroutine(AllowJumpingAgain());
            }
        }
    }

    private IEnumerator AllowJumpingAgain()
    {
        yield return new WaitForSeconds(jumpCooldown);
        _jumped = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_colliderComponent.IsTouchingLayers(LayerMask.GetMask(LadderLayer)) && !_jumped)
        {
            if (Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Vertical") > 0 && !_canClimb)
            {
                _canClimb = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StopClimbing();
    }

    private void StopClimbing()
    {
        _canClimb = false;
        _rigidbodyComponent.gravityScale = 1f;
        _animatorComponent.SetBool(_climbing, false);
        _animatorComponent.speed = 1f;
    }
}