using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CollisionHandler))]
public class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _startSpeed;

    private Rigidbody2D _rigidbody;
    private CollisionHandler _collisionHandler;

    public event Action<Bullet> DispawnNeeded;

    public Rigidbody2D Rigidbody => _rigidbody;
    public float StartSpeed => _startSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= ProcessCollision;
    }

    private void ProcessCollision(IInteractable _)
    {
        DispawnNeeded?.Invoke(this);
    }
}