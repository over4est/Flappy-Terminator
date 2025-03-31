using System;

public class Enemy : Character, IInteractable
{
    public event Action<Enemy> DispawnNeeded;
    public event Action Killed;

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
        Attacker.Attack();
    }

    public override void Reset()
    {
        if (Attacker != null)
            Attacker.Reset();
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