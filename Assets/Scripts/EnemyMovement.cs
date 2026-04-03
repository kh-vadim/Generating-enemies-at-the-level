using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class EnemyMovement : MonoBehaviour
{
    public event Action<EnemyMovement> ObstacleHit;

    [SerializeField] private float _speed = 5f;

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WallMarker _))
            ObstacleHit?.Invoke(this);
    }
}
