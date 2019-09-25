using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private GameSession gameSession;
    
    private TextMeshProUGUI _textComponent;

    private void Start()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _textComponent.text = $"Score: {gameSession.PlayerScore}";
    }
}
