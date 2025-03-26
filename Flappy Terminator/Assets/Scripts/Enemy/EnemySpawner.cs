using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawnTimer), typeof(ScoreCounter))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _yOffset;
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _enemyCount;

    private ObjectPool<Enemy> _pool;
    private List<Enemy> _enemies;
    private EnemySpawnTimer _timer;
    private ScoreCounter _scoreCounter;

    public void Reset()
    {
        foreach (Enemy enemy in _enemies)
            enemy.Reset();

        _pool.ReleaseAll();
        _scoreCounter.Reset();
    }

    private void Awake()
    {
        _scoreCounter = GetComponent<ScoreCounter>();
        _timer = GetComponent<EnemySpawnTimer>();
        _pool = new ObjectPool<Enemy>(_prefab, _enemyCount, transform);
        _enemies = _pool.GetAllObjects();
    }

    private void Start()
    {
        _timer.StartTimer();
    }

    private void OnEnable()
    {
        EnemySubscribe(_enemies);

        _timer.TimerTicked += Spawn;
    }

    private void OnDisable()
    {
        EnemyUnsubscribe(_enemies);

        _timer.TimerTicked -= Spawn;
    }

    private void Spawn()
    {
        if (_pool.TryGet(out Enemy enemy))
        {
            float randomYOffset = Random.Range(-_yOffset, _yOffset);

            enemy.transform.position = new Vector3(_spawnPoint.position.x, _spawnPoint.position.y + randomYOffset, 0f);
        }
    }

    private void Release(Enemy enemy)
    {
        _pool.Release(enemy);
    }

    private void EnemySubscribe(List<Enemy> objects)
    {
        foreach (Enemy enemy in objects)
        {
            enemy.DispawnNeeded += Release;
            enemy.Killed += AddScore;
        }
    }

    private void EnemyUnsubscribe(List<Enemy> objects)
    {
        foreach (Enemy enemy in objects)
        {
            enemy.DispawnNeeded -= Release;
            enemy.Killed -= AddScore;
        }
    }

    private void AddScore()
    {
        _scoreCounter.Add();
    }
}