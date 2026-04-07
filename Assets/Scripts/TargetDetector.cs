using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TargetDetector : MonoBehaviour
{
    public event Action TargetReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TargetMarker _))
            TargetReached?.Invoke();
    }
}
