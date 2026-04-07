using System;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(TargetDetector))]
public class Enemy : MonoBehaviour
{
    private EnemyMover _mover;
    private TargetDetector _detector;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _detector = GetComponent<TargetDetector>();
    }

    private void OnEnable()
    {
        _detector.TargetReached += TriggerDeath;
    }

    private void OnDisable()
    {
        _detector.TargetReached -= TriggerDeath;
    }

    public void Initialize(Transform target)
    {
        _mover.MoveTo(target);
    }

    private void TriggerDeath()
    {
        Died?.Invoke(this);
    }
}
