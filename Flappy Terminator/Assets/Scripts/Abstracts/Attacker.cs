using System.Collections;
using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _reloadTime;

    private ObjectPool<Bullet> _pool;
    private WaitForSeconds _wait;
    private bool _isReloaded;
    private BulletContainer _bulletContainer;

    protected ObjectPool<Bullet> Pool => _pool;
    protected Transform AttackPoint => _attackPoint;
    protected bool IsReloaded => _isReloaded;

    public void Reset()
    {
        _pool.ReleaseAll();
    }

    private void Awake()
    {
        _bulletContainer = GetComponentInParent<BulletContainer>();
        _pool = _bulletContainer.Pool;
        _wait = new WaitForSeconds(_reloadTime);
    }

    private void OnEnable()
    {
        _isReloaded = true;
    }

    public abstract void Attack();

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
}