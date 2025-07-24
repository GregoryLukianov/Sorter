using Events;
using Events.Handlers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class Menu : MonoBehaviour, IGameplayHandler
    {
        [SerializeField] private CanvasGroup _menuPanel;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private Image _backgroundImage;

        [Inject] private EventBus _eventBus;

        private void Awake()
        {
            _eventBus.Subscribe(this);
            
            TurnOn();
        }

        private void TurnOn()
        {
            _menuPanel.alpha = 1;
            _menuPanel.blocksRaycasts = true;
        }

        private void TurnOff()
        {
            _menuPanel.alpha = 0;
            _menuPanel.blocksRaycasts = false;
        }

        public void StartGame()
        {
            _eventBus.RaiseEvent<IGameplayHandler>(h => h.HandleGameplayStart());
        }

        public void HandleGameplayStart()
        {
            TurnOff();
        }

        public void HandleGameOver()
        {
            TurnOn();
            _titleText.text = "game over";
            _backgroundImage.color = Color.red;
            _scoreText.text = "";
            _buttonText.text = "restart";
        }

        public void HandleWin(int score)
        {
            TurnOn();
            _titleText.text = "victory";
            _backgroundImage.color = Color.green;
            _scoreText.text = $"Score: {score}";
            _buttonText.text = "restart";
        }
    }
}