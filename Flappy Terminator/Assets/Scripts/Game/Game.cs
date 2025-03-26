using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private EnemySpawner _enemySpawner;

    private void OnEnable()
    {
        _startScreen.ButtonClicked += OnPlayButtonClick;
        _gameOverScreen.ButtonClicked += OnRestartButtonClick;
        _player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _startScreen.ButtonClicked -= OnPlayButtonClick;
        _gameOverScreen.ButtonClicked -= OnRestartButtonClick;
        _player.GameOver -= OnGameOver;
    }

    private void StartGame()
    {
        Time.timeScale = 1;

        _enemySpawner.Reset();
        _player.Reset();
    }

    private void Start()
    {
        Time.timeScale = 0f;

        _startScreen.Open();
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void OnRestartButtonClick()
    {
        _gameOverScreen.Close();
        StartGame();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0f;

        _player.DisableInput();
        _gameOverScreen.Open();
    }
}