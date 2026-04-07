using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 2f;

    [Tooltip("С какой точки начать? (от 0 до количества точек минус 1")]
    [SerializeField] private int _startingIndex = 0;

    private int _currentIndex;

    private void Start()
    {
        if (_waypoints == null || _waypoints.Length == 0)
        {
            Debug.LogWarning($"У объекта {gameObject.name} не настроен маршрут! Скрипт отключен.");
            enabled = false;
            return;
        }

        _startingIndex = Mathf.Clamp(_startingIndex, 0, _waypoints.Length - 1);
        transform.position = _waypoints[_startingIndex].position;
        _currentIndex = (_startingIndex + 1) % _waypoints.Length;
    }

    private void Update()
    {
        Transform currentWaypoint = _waypoints[_currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, _speed * Time.deltaTime);

        if ((transform.position - currentWaypoint.position).sqrMagnitude < Mathf.Epsilon)
            _currentIndex = (_currentIndex +1) % _waypoints.Length;
    }
}
