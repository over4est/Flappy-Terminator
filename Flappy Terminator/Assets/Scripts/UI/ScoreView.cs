using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _scoreCounter.ScoreChanged += ChangeText;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChanged += ChangeText;
    }

    private void ChangeText(int value)
    {
        _text.text = value.ToString();
    }
}