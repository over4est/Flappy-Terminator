using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerMover), typeof(PlayerRotator))]
public class Player : Character
{
    private InputReader _inputReader;
    private PlayerMover _playerMover;
    private PlayerRotator _playerRotator;

    public event Action GameOver;

    private new void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerMover = GetComponent<PlayerMover>();
        _playerRotator = GetComponent<PlayerRotator>();

        base.Awake();
    }

    private void OnEnable()
    {
        _inputReader.AttackButtonPressed += Attack;
        _inputReader.MoveButtonPressed += Move;
        CollisionHandler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _inputReader.AttackButtonPressed -= Attack;
        _inputReader.MoveButtonPressed -= Move;
        CollisionHandler.CollisionDetected -= ProcessCollision;
    }

    public override void Reset()
    {
        _inputReader.SetGameStarted();
        _playerMover.Reset();
        Attacker.Reset();
    }

    public override void Attack()
    {
        Attacker.Attack();
    }

    public void DisableInput()
    {
        _inputReader.SetGameEnded();
    }

    private void Move()
    {
        _playerMover.Move();
        _playerRotator.RotateToMax();
    }

    private void ProcessCollision(IInteractable obj)
    {
        if (obj is DispawnZone)
            return;

        GameOver?.Invoke();
    }
}