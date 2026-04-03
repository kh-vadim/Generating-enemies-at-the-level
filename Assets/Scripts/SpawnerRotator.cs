using UnityEngine;

public class SpawnerRotator : MonoBehaviour
{
    private Vector3 _rotationSpeed;

    private void Start()
    {
        _rotationSpeed = new Vector3(0, Random.Range(-90f, 90f), 0);
    }

    private void Update()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime);
    }
}
