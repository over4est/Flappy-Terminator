using UnityEngine;

[RequireComponent(typeof(CollisionHandler), typeof (Attacker))]
public abstract class Character : MonoBehaviour
{
    private CollisionHandler _collisionHandler;
    private Attacker _attacker;

    protected CollisionHandler CollisionHandler => _collisionHandler;
    protected Attacker Attacker => _attacker;

    protected void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        _attacker = GetComponent<Attacker>();
    }

    public abstract void Reset();

    public abstract void Attack();
}