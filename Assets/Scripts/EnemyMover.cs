using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Transform _target;

    public void MoveTo(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null) return;

        Vector3 direction = _target.position - transform.position;

        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }    
}
