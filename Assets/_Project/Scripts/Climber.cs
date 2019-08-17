using UnityEngine;

public class Climber : MonoBehaviour
{
    [SerializeField] private float climbingSpeed;

    private const string LadderLayer = "Ladder";

    private Collider2D _colliderComponent;
    private Rigidbody2D _rigidbodyComponent;

    private bool _canClimb;

    private void Start()
    {
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
            _rigidbodyComponent.gravityScale = 0f;
            _rigidbodyComponent.velocity = new Vector2(_rigidbodyComponent.velocity.x, direction* climbingSpeed);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_colliderComponent.IsTouchingLayers(LayerMask.GetMask(LadderLayer)))
        {
            if (Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Vertical") > 0 && !_canClimb)
            {
                _canClimb = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_colliderComponent.IsTouchingLayers(LayerMask.GetMask(LadderLayer)))
        {
            Debug.Log("Not climbing anymore!");
            _canClimb = false;
            _rigidbodyComponent.gravityScale = 1f;
        }
    }
}
