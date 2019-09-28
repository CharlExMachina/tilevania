using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSessionController : MonoBehaviour
{
    [SerializeField] private GameSession gameSession;

    private SceneLoader _sceneLoaderComponent;

    public UnityEvent onSessionReset;

    private void Start()
    {
        _sceneLoaderComponent = GetComponent<SceneLoader>();
        gameSession.InitGame();
    }

    public void HandlePlayerDeath()
    {
        if (gameSession.PlayerLives > 1)
        {
            TakePlayerLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void OnApplicationQuit()
    {
        gameSession.ResetSession();
    }

    private void TakePlayerLife()
    {
        gameSession.LoseLife();
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        _sceneLoaderComponent.LoadScene(currentSceneIndex);
    }

    private void ResetGameSession()
    {
        onSessionReset.Invoke();
        gameSession.ResetSession();
        _sceneLoaderComponent.LoadScene("Game Over");
    }
}
