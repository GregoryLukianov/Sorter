using Events;
using Events.Handlers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class HealthPointsUI: MonoBehaviour, IHealthPointsChangeHandler
    {
        [SerializeField] private Text _healthPointsText;
        [SerializeField] private Image _filledBackgorund;
        [Inject] private EventBus _eventBus;
        
        
        private void Awake()
        {
            _eventBus.Subscribe(this);
        }
        
        public void HandleHealthPointsChange(int healthPointsLeft, int healthPoints)
        {
            _healthPointsText.text = $"Health: {healthPointsLeft}";
            _filledBackgorund.fillAmount = (float)healthPointsLeft / (float)healthPoints;
        }
    }
}