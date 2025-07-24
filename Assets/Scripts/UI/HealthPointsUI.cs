using Events;
using Events.Handlers;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class HealthPointsUI: MonoBehaviour, IHealthPointsChangeHandler
    {
        [SerializeField] private TextMeshProUGUI _healthPointsText;
        [Inject] private EventBus _eventBus;
        
        
        private void Awake()
        {
            _eventBus.Subscribe(this);
        }
        
        public void HandleHealthPointsChange(int healthPointsLeft)
        {
            _healthPointsText.text = $"Health: {healthPointsLeft}";
        }
    }
}