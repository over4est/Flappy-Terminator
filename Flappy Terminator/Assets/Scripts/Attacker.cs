using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _reloadTime;

    private ObjectPool<Bullet> _pool;
    private WaitForSeconds _waitForAttack;
    private bool _isReloaded;
    private BulletContainer _bulletContainer;

    public void Reset()
    {
        if (_pool != null)
        {
            _pool.ReleaseAll();
        }
    }

    private void Awake()
    {
        _bulletContainer = GetComponentInParent<BulletContainer>();
        _waitForAttack = new WaitForSeconds(_reloadTime);
    }

    private void Start()
    {
        _pool = _bulletContainer.Pool;
    }

    private void OnEnable()
    {
        _isReloaded = true;
    }

    public void Attack()
    {
        if (_isReloaded && _pool.TryGet(out Bullet bullet))
        {
            bullet.transform.position = _attackPoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.Rigidbody.velocity = transform.right * bullet.StartSpeed;

            Reload();
        }
    }

    private void Reload()
    {
        _isReloaded = false;

        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        yield return _waitForAttack;

        _isReloaded = true;
    }
}