using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    [SerializeField] private Bullet _prefab;
    [SerializeField] private int _bulletAmount;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _reloadTime;

    private BulletContainer _container;
    private ObjectPool<Bullet> _pool;
    private WaitForSeconds _wait;
    private bool _isReloaded;
    private List<Bullet> _bullets;

    protected ObjectPool<Bullet> Pool => _pool;
    protected Transform AttackPoint => _attackPoint;
    protected bool IsReloaded => _isReloaded;

    public abstract void Attack();

    public void Reset()
    {
        _pool.ReleaseAll();
    }

    protected void Reload()
    {
        _isReloaded = false;

        StartCoroutine(Countdown(_wait));
    }

    private IEnumerator Countdown(WaitForSeconds wait)
    {
        yield return wait;

        _isReloaded = true;
    }

    private void Awake()
    {
        _container = GetComponentInParent<BulletContainer>();
        _pool = new ObjectPool<Bullet>(_prefab, _bulletAmount, _container.transform);
        _wait = new WaitForSeconds(_reloadTime);
        _bullets = _pool.GetAllObjects();
    }

    private void OnEnable()
    {
        _isReloaded = true;

        Subscribe(_bullets);
    }

    private void OnDisable()
    {
        Unsubscribe(_bullets);
    }

    private void Subscribe(List<Bullet> bullets)
    {
        foreach (Bullet bullet in bullets)
        {
            bullet.DispawnNeeded += Release;
        }
    }

    private void Unsubscribe(List<Bullet> bullets)
    {
        foreach (Bullet bullet in bullets)
        {
            bullet.DispawnNeeded -= Release;
        }
    }

    private void Release(Bullet bullet)
    {
        _pool.Release(bullet);
    }
}