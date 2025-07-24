using Events.Handlers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class VictoryMenu : MenuPanel, IVictoryHandler
    {
        [SerializeField] private Text _scoreText;

        
        protected override void Awake()
        {
            base.Awake();
            _eventBus.Subscribe(this);
        }

        public void RestartGame()
        {
            _eventBus.RaiseEvent<IGameplayHandler>(h => h.HandleGameplayStart());
            TurnOff();
        }

        public void HandleVictory(int score)
        {
            TurnOnWithDelay(3f);
            _scoreText.text = $"Score: {score}";
        }
    }
}