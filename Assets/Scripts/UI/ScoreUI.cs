using Events;
using Events.Handlers;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ScoreUI: MonoBehaviour, IScoreChangeHandler
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [Inject] private EventBus _eventBus;
        
        private void Awake()
        {
            _eventBus.Subscribe(this);
        }

        public void HandleScoreChange(int score)
        {
            _scoreText.text = $"Score: {score}";
        }
    }
}