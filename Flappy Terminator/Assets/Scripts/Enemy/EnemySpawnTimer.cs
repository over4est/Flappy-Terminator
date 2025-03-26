using System;
using System.Collections;
using UnityEngine;

public class EnemySpawnTimer : MonoBehaviour
{
    [SerializeField] private float _minSpawnDelay;
    [SerializeField] private float _maxSpawnDelay;

    public event Action TimerTicked;

    public void StartTimer()
    {
        StartCoroutine(Countdown(_minSpawnDelay, _maxSpawnDelay));
    }

    private IEnumerator Countdown(float minDelay, float maxDelay)
    {
        while (enabled)
        {
            float delay = UnityEngine.Random.Range(minDelay, maxDelay);
            WaitForSeconds wait = new WaitForSeconds(delay);

            yield return wait;

            TimerTicked?.Invoke();
        }
    }
}