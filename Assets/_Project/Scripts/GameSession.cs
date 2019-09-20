using UnityEngine;

[CreateAssetMenu(fileName = "New game session", menuName = "Tools/New game session", order = 0)]
public class GameSession : ScriptableObject
{
    [SerializeField] private int startingPlayerLives = 3;

    private int _playerLives;
    private bool _sessionStarted;

    public int PlayerLives => _playerLives;

    public void ResetSession()
    {
        _sessionStarted = false;
    }

    public void InitGame()
    {
        if (_sessionStarted) return;
        
        _sessionStarted = true;
        _playerLives = startingPlayerLives;
    }
    
    public void LoseLife()
    {
        _playerLives--;
    }
}
