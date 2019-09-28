using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform explosionParticles;
    
    private Vector3 _movementDirection;
    
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(explosionParticles, transform.position - _movementDirection * 0.5f, Quaternion.Euler(90, 0, 0));
        Destroy(gameObject);
    }
}
