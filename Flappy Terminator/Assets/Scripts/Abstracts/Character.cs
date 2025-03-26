using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public abstract class Character : MonoBehaviour, IDamageable
{
    private CollisionHandler _collisionHandler;

    protected CollisionHandler CollisionHandler => _collisionHandler;

    public abstract void Reset();

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    public abstract void TakeDamage();

    public abstract void Attack();

}