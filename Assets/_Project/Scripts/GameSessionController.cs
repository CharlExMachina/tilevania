using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessionController : MonoBehaviour
{
    [SerializeField] private GameSession gameSession;

    private SceneLoader _sceneLoaderComponent;

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
        Debug.Log($"Player lost a life! Lives remaining: {gameSession.PlayerLives}");
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        _sceneLoaderComponent.LoadScene(currentSceneIndex);
    }

    private void ResetGameSession()
    {
        gameSession.ResetSession();
        _sceneLoaderComponent.LoadScene(0);
    }
}
