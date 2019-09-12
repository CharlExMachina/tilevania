using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    
    private Rigidbody2D _rigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        _rigidbodyComponent.MovePosition(transform.position + Time.deltaTime * movementSpeed * Vector3.right);
    }
}
