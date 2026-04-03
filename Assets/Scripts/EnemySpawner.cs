using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [Header("Настройка спавнера")]

    [SerializeField] private EnemyMovement _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnDelay = 2f;

    [Header("Настройка пула объектов")]
    [Tooltip("Начальное количество врагов (сколько создать сразу для готовности)")]
    [SerializeField] private int _pollDefaultCapacity = 10;
    [Tooltip("Максимальное количество включенных врагов в резерве пула")]
    [SerializeField] private int _poolMaxSize = 50;

    private ObjectPool<EnemyMovement> _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool<EnemyMovement>(
            createFunc: CreateEnemy,
            actionOnGet: EnableEnemy,
            actionOnRelease: DisableEnemy,
            actionOnDestroy: DestroyEnemy,
            defaultCapacity: _pollDefaultCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemyCoroutine())  ;
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            yield return wait;

            int randomIndex = Random.Range(0, _spawnPoints.Length);
            Transform randomSpawnPoint = _spawnPoints[randomIndex];

            EnemyMovement enemy = _enemyPool.Get();
            enemy.transform.position = randomSpawnPoint.position;
            enemy.transform.rotation = randomSpawnPoint.rotation;
        }
    }

    private EnemyMovement CreateEnemy()
    {
        return Instantiate(_enemyPrefab);
    }

    private void EnableEnemy(EnemyMovement enemy)
    {
        enemy.ObstacleHit += ReturnEnemyToPoll;
        enemy.gameObject.SetActive(true);
    }

    private void DisableEnemy(EnemyMovement enemy)
    {
        enemy.ObstacleHit -= ReturnEnemyToPoll;
        enemy.gameObject.SetActive(false);
    }

    private void DestroyEnemy(EnemyMovement enemy)
    {
        Destroy(enemy.gameObject);
    }

    private void ReturnEnemyToPoll(EnemyMovement enemy)
    {
        _enemyPool.Release(enemy);
    }
}
