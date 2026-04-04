using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [Header("Настройка спавнера")]
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnDelay = 2f;

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

    private void OnEnable()
    {
        StartCoroutine(SpawningEnemies());
    }

    private IEnumerator SpawningEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            yield return wait;

            int randomIndex = Random.Range(0, _spawnPoints.Length);
            Transform randomSpawnPoint = _spawnPoints[randomIndex];

            Enemy enemy = _enemyPool.Get();
            enemy.transform.position = randomSpawnPoint.position;

            float randomAngle = Random.Range(0f, 360f);

            Vector3 randomDirection = Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward;

            enemy.Initialize(randomDirection);
        }
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
