using System;
using UnityEngine;

public class Climber : MonoBehaviour
{
    [SerializeField] private float climbingSpeed;

    private const string LadderLayer = "Ladder";

    private Collider2D _colliderComponent;

    private bool _canClimb;

    private void Start()
    {
        _colliderComponent = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_colliderComponent.IsTouchingLayers(LayerMask.GetMask(LadderLayer)))
        {
            _canClimb = true;
        }
    }
}
