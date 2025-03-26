using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawnTimer), typeof(ScoreCounter))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _yOffset;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _enemyCount;

    private ObjectPool<Enemy> _enemyPool;
    private List<Enemy> _enemies;
    private EnemySpawnTimer _timer;
    private ScoreCounter _scoreCounter;

    public void Reset()
    {
        foreach (Enemy enemy in _enemies)
            enemy.Reset();

        _enemyPool.ReleaseAll();
        _scoreCounter.Reset();
    }

    private void Awake()
    {
        _scoreCounter = GetComponent<ScoreCounter>();
        _timer = GetComponent<EnemySpawnTimer>();
        _enemyPool = new ObjectPool<Enemy>(_enemyPrefab, _enemyCount, transform);
        _enemies = _enemyPool.GetAllObjects();
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
        if (_enemyPool.TryGet(out Enemy enemy))
        {
            float randomYOffset = Random.Range(-_yOffset, _yOffset);

            enemy.transform.position = new Vector3(_spawnPoint.position.x, _spawnPoint.position.y + randomYOffset, 0f);
        }
    }

    private void Release(Enemy enemy)
    {
        _enemyPool.Release(enemy);
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