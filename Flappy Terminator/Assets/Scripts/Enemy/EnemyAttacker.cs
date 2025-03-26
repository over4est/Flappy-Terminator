using UnityEngine;

public class EnemyAttacker : Attacker
{
    private Vector2 _attackVector = new Vector2(-1f, 0f);

    public override void Attack()
    {
        if (IsReloaded && Pool.TryGet(out Bullet bullet))
        {
            bullet.transform.position = AttackPoint.transform.position;
            bullet.Rigidbody.velocity = _attackVector * bullet.StartSpeed;

            Reload();
        }
    }
}