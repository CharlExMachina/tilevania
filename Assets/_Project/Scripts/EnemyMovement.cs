using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private Vector3 initialDirection;

    private Vector3 _direction;
    private Rigidbody2D _rigidbodyComponent;
    private Collider2D _colliderComponent;
    
    // Start is called before the first frame update
    void Start()
    {
        _colliderComponent = GetComponent<Collider2D>();
        _direction = initialDirection;
        _rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        CheckForWalls();
        _rigidbodyComponent.MovePosition(transform.position + Time.deltaTime * movementSpeed * _direction);
    }

    private void CheckForWalls()
    {
        var rayOrigin = Vector2.zero;

        if (_direction.x > 0) // right
        {
            rayOrigin = new Vector2(_colliderComponent.bounds.max.x, 0);
        }
        else if (_direction.x < 0)
        {
            rayOrigin = new Vector2(_colliderComponent.bounds.min.x, 0);
        }
        
        var filter = new ContactFilter2D
        {
            layerMask = LayerMask.GetMask("Ground"),
            useLayerMask = true,
            useTriggers = true
        };
        
        var hit = Physics2D.Raycast(transform.position, _direction, 1f, LayerMask.GetMask("Ground"));
        
        if (hit)
        {
            Debug.Log($"There's a wall ahead, changing direction! {hit.collider.gameObject.layer}");
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        _direction *= -1;
    }
}
