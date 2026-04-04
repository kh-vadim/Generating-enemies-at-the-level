using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class ObstacleDetector : MonoBehaviour
{
    public event Action ObstacleHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WallMarker _))
            ObstacleHit?.Invoke();
    }
}
