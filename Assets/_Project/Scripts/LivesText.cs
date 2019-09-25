using TMPro;
using UnityEngine;

public class LivesText : MonoBehaviour
{
        [SerializeField] private GameSession gameSession;
        
        private TextMeshProUGUI _textComponent;

        private void Start()
        {
            _textComponent = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _textComponent.text = $"Lives: {gameSession.PlayerLives}";
        }
}
