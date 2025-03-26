using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAttacker))]
public class Enemy : Character, IInteractable
{
    private EnemyAttacker _enemyAttacker;

    public event Action<Enemy> DispawnNeeded;
    public event Action Killed;

    private void Start()
    {
        _enemyAttacker = GetComponent<EnemyAttacker>();
    }

    private void Update()
    {
        Attack();
    }

    private void OnEnable()
    {
        CollisionHandler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        CollisionHandler.CollisionDetected -= ProcessCollision;
    }

    public override void Attack()
    {
        _enemyAttacker.Attack();
    }

    public override void TakeDamage()
    {
        DispawnNeeded?.Invoke(this);
    }

    public override void Reset()
    {
        if (_enemyAttacker != null)
            _enemyAttacker.Reset();
    }

    private void ProcessCollision(IInteractable obj)
    {
        if (obj is Bullet)
        {
            DispawnNeeded?.Invoke(this);
            Killed?.Invoke();
        }
        else if (obj is DispawnZone)
        {
            DispawnNeeded?.Invoke(this);
        }
    }
}