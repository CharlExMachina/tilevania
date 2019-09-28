using TreeEditor;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Vector2 _movementDirection;
    
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetDirection(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void Move()
    {
        transform.Translate(Time.deltaTime * movementSpeed * _movementDirection, Space.World);
    }
}
