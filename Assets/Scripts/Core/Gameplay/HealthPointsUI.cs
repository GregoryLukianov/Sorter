using Core.Events;
using Core.Events.Handlers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Gameplay
{
    public class HealthPointsUI: MonoBehaviour, IUpdateHealthPointsUIHandler
    {
        [SerializeField] private TextMeshProUGUI _healthPointsText;
        [Inject] private EventBus _eventBus;
        
        
        private void Awake()
        {
            _eventBus.Subscribe(this);
        }
        
        public void UpdateHealthPoints(int healthPointsLeft)
        {
            _healthPointsText.text = $"Health: {healthPointsLeft}";
        }
    }
}