using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerMover), typeof(PlayerRotator))]
[RequireComponent(typeof(PlayerAttacker))]
public class Player : Character
{
    public event Action GameOver;

    private InputReader _inputReader;
    private PlayerMover _playerMover;
    private PlayerRotator _playerRotator;
    private PlayerAttacker _attacker;

    public override void Reset()
    {
        _inputReader.SetGameStarted();
        _playerMover.Reset();
        _attacker.Reset();
    }

    private void Start()
    {
        _inputReader = GetComponent<InputReader>();
        _playerMover = GetComponent<PlayerMover>();
        _playerRotator = GetComponent<PlayerRotator>();
        _attacker = GetComponent<PlayerAttacker>();

        Subscribe();
    }

    private void OnDisable()
    {
        _inputReader.AttackButtonPressed -= Attack;
        _inputReader.MoveButtonPressed -= Move;
        CollisionHandler.CollisionDetected -= ProcessCollision;
    }

    public override void TakeDamage()
    {
        GameOver?.Invoke();
    }

    public override void Attack()
    {
        _attacker.Attack();
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
        if (obj is Ground || obj is Sky || obj is Enemy || obj is Bullet)
            GameOver?.Invoke();
    }

    private void Subscribe()
    {
        _inputReader.AttackButtonPressed += Attack;
        _inputReader.MoveButtonPressed += Move;
        CollisionHandler.CollisionDetected += ProcessCollision;
    }
}