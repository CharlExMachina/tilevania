using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New game session", menuName = "Tools/New game session", order = 0)]
public class GameSession : ScriptableObject
{
    [SerializeField] private int startingPlayerLives = 3;
    [SerializeField] private int startingPlayerScore = 0;

    private int _playerLives;
    private int _playerScore;
    private bool _sessionStarted;

    public int PlayerLives => _playerLives;

    public int PlayerScore => _playerScore;
    
    public void ResetSession()
    {
        if (_sessionStarted)
            _sessionStarted = false;
    }

    public void InitGame()
    {
        if (_sessionStarted) return;
        
        _sessionStarted = true;
        _playerLives = startingPlayerLives;
        _playerScore = startingPlayerScore;
    }
    
    public void LoseLife()
    {
        _playerLives--;
    }

    public void ScorePoints(int pointsToScore)
    {
        _playerScore += pointsToScore;
    }
}
