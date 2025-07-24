using System.Collections.Generic;
using Events;
using Events.Handlers;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameStarter: MonoBehaviour, IGameplayHandler
    {
        [SerializeField] private List<Path> _paths;
        [Inject] private DiContainer _container;
        [Inject] private EventBus _eventBus;
        
        public Gameplay CurrentGameplaySession { get; private set; }
        

        private void Awake()
        {
            _eventBus.Subscribe(this);
        }

        public void HandleGameplayStart()
        {
            CurrentGameplaySession = _container.Instantiate<Gameplay>();
            CurrentGameplaySession.Init(_paths);
            CurrentGameplaySession.StartGameplaySession();
        }
    }
}