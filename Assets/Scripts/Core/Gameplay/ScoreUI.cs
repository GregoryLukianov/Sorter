using System;
using Core.Events;
using Core.Events.Handlers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Gameplay
{
    public class ScoreUI: MonoBehaviour, IUpdateScoreUIHandler
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [Inject] private EventBus _eventBus;
        
        private void Awake()
        {
            _eventBus.Subscribe(this);
        }

        public void UpdateScoreUI(int score)
        {
            _scoreText.text = $"Score: {score}";
        }
    }
}