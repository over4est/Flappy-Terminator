using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _scoreAmount;

    public event Action<int> ScoreChanged;

    public void Reset()
    {
        _scoreAmount = 0;

        ScoreChanged?.Invoke(_scoreAmount);
    }

    public void Add()
    {
        _scoreAmount++;

        ScoreChanged?.Invoke(_scoreAmount);
    }
}