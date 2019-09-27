using UnityEngine;

public class RisingWater : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;

    private void Update()
    {
        var newPos = new Vector3(0, speed * Time.deltaTime);
        transform.position += newPos;
        target.position += newPos;
    }
}
