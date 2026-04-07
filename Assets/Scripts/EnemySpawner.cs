using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [Header("Настройка спавнера")]
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private TargetMarker[] _targets;

    [Header("Настройка пула объектов")]
    [Tooltip("Начальное количество врагов")]
    [SerializeField] private int _poolDefaultCapacity = 10;
    [Tooltip("Максимальное количество включенных врагов")]
    [SerializeField] private int _poolMaxSize = 50;

    private ObjectPool<Enemy> _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(
            createFunc: CreateEnemy,
            actionOnGet: EnableEnemy,
            actionOnRelease: DisableEnemy,
            actionOnDestroy: DestroyEnemy,
            defaultCapacity: _poolDefaultCapacity,
            maxSize: _poolMaxSize
            );
    }

    public void SpawnRandomEnemy()
    {
        if (_targets == null || _targets.Length == 0) return;

        TargetMarker randomTarget = _targets[Random.Range(0, _targets.Length)];

        Enemy enemy = _enemyPool.Get();
        enemy.transform.position = transform.position;

        enemy.Initialize(randomTarget.transform);
    }

    private Enemy CreateEnemy()
    {
        return Instantiate(_enemyPrefab);
    }

    private void EnableEnemy(Enemy enemy)
    {
        enemy.Died += ReturnEnemyToPool;
        enemy.gameObject.SetActive(true);
    }

    private void DisableEnemy(Enemy enemy)
    {
        enemy.Died -= ReturnEnemyToPool;
        enemy.gameObject.SetActive(false);
    }

    private void DestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    private void ReturnEnemyToPool(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }
}
