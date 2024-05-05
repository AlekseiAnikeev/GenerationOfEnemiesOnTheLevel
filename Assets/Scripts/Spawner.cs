using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [Space(10)] [SerializeField, Min(1f)] private float _repeatRate = 2f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Enemy> _pool;
    private List<Vector3> _spawnCoordinate;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemy),
            actionOnGet: enemy => enemy.gameObject.SetActive(true),
            actionOnRelease: enemy => enemy.gameObject.SetActive(false),
            actionOnDestroy: Destroy,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );

        _spawnCoordinate = new List<Vector3>()
        {
            new(45,0,45),
            new(-45,0,45),
            new(45,0,-45),
        };
    }

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, _repeatRate);
    }

    private void Spawn()
    {
            var enemy = _pool.Get();
            enemy.transform.position = GetRandomPosition();
            enemy.SetTargetPosition(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            _pool.Release(enemy);
        }
    }

    private Vector3 GetRandomPosition()
    {
        var minCoordinateRate = 0;
        var maxCoordinateRate = _spawnCoordinate.Count;

        return _spawnCoordinate[Random.Range(minCoordinateRate, maxCoordinateRate)];
    }
}