using Events;
using Events.Handlers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class GameOver : MenuPanel, IGameOverHandler
    {
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
        
        public void HandleGameOver()
        {
            TurnOn();
        }
    }
}