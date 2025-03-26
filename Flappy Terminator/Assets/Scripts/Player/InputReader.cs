using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Move = nameof(Move);
    private const string Fire1 = nameof(Fire1);

    private bool _isGameStarted = false;

    public event Action MoveButtonPressed;
    public event Action AttackButtonPressed;

    public void SetGameStarted()
    {
        _isGameStarted = true;
    }

    public void SetGameEnded()
    {
        _isGameStarted = false;
    }

    private void Update()
    {
        if (_isGameStarted)
        {
            if (Input.GetButtonDown(Move))
            {
                MoveButtonPressed?.Invoke();
            }

            if (Input.GetButtonDown(Fire1))
            {
                AttackButtonPressed?.Invoke();
            }
        }
    }
}