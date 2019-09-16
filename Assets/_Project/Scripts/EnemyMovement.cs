using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private Vector3 initialDirection;

    private Vector3 _direction;
    private Rigidbody2D _rigidbodyComponent;
    private Collider2D _colliderComponent;
    private SpriteRenderer _spriteComponent;

    // Start is called before the first frame update
    void Start()
    {
        _direction = initialDirection;
        _spriteComponent = GetComponent<SpriteRenderer>();
        _colliderComponent = GetComponent<Collider2D>();
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
        var xOffset = 0.0f;

        if (_direction.x < 0)
        {
            xOffset = -0.5f;
        }
        else if (_direction.x > 0)
        {
            xOffset = 0.5f;
        }
        
        var position = transform.position;
        var wallHit = Physics2D.Raycast(position, _direction, 0.3f, LayerMask.GetMask("Ground"));
        var groundPosition = new Vector3(position.x + xOffset, position.y);
        var ground = Physics2D.Raycast(groundPosition, Vector2.down, 0.8f, LayerMask.GetMask("Ground"));

        if (wallHit || !ground)
        {
//            Debug.Log($"There's a wall ahead, changing direction! {wallHit.collider.gameObject.layer}");
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        _direction *= -1;
        _spriteComponent.flipX = !_spriteComponent.flipX;
    }
}