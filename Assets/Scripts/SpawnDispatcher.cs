using System.Collections;
using UnityEngine;

public class SpawnDispatcher : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _spawners;
    [SerializeField] private float _spawnDelay = 2f;

    private void OnEnable()
    {
        StartCoroutine(DispatchingSpawns());
    }

    private IEnumerator DispatchingSpawns()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            yield return wait;

            if (_spawners.Length > 0)
            {
                int randomIndex = Random.Range(0, _spawners.Length);

                _spawners[randomIndex].SpawnRandomEnemy();
            }
        }
    }
}
