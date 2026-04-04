using System;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(ObstacleDetector))]
public class Enemy : MonoBehaviour
{
    private EnemyMover _mover;
    private ObstacleDetector _detector;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _detector = GetComponent<ObstacleDetector>();
    }

    private void OnEnable()
    {
        _detector.ObstacleHit += TriggerDeath;
    }

    private void OnDisable()
    {
        _detector.ObstacleHit -= TriggerDeath;
    }

    public void Initialize(Vector3 direction)
    {
        _mover.RotateAndMove(direction);
    }

    private void TriggerDeath()
    {
        Died?.Invoke(this);
    }
}
