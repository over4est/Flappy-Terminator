using System.Collections.Generic;
using UnityEngine;

public class BulletContainer : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _bulletCount;

    private ObjectPool<Bullet> _pool;
    private List<Bullet> _bullets;

    public ObjectPool<Bullet> Pool => _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(_bulletPrefab, _bulletCount, transform);
        _bullets = _pool.GetAllObjects();
    }

    private void OnEnable()
    {
        foreach(Bullet bullet in _bullets)
        {
            bullet.DispawnNeeded += Release;
        }
    }

    private void OnDisable()
    {
        foreach (Bullet bullet in _bullets)
        {
            bullet.DispawnNeeded -= Release;
        }
    }

    private void Release(Bullet bullet)
    {
        _pool.Release(bullet);
    }
}