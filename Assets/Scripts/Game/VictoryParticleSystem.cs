using System.Collections.Generic;
using Events;
using Events.Handlers;
using UnityEngine;
using Zenject;

namespace Game
{
    public class VictoryParticleSystem: MonoBehaviour, IVictoryHandler, IGameplayHandler
    {
        [SerializeField] private List<ParticleSystem> _particleSystems;

        [Inject] private EventBus _eventBus;


        private void Awake()
        {
            _eventBus.Subscribe(this);
        }

        public void HandleVictory(int score)
        {
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.Play();
            }
        }

        public void HandleGameplayStart()
        {
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.Stop();
            }
        }
    }
}