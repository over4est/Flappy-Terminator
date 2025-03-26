public class PlayerAttacker : Attacker
{
    public override void Attack()
    {
        if (IsReloaded && Pool.TryGet(out Bullet bullet))
        {
            bullet.transform.position = AttackPoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.Rigidbody.velocity = (AttackPoint.position - transform.position).normalized * bullet.StartSpeed;

            Reload();
        }
    }
}