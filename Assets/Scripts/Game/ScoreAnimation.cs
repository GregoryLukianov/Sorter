using System;
using Events;
using Events.Handlers;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ScoreAnimation: MonoBehaviour, IAddScoreHandler
    {
        [SerializeField] private Animator _animator;

        [Inject] private EventBus _eventBus;


        private void Awake()
        {
            _eventBus.Subscribe(this);
        }

        public void HandleAddScore()
        {
            _animator.SetTrigger("Bounce");
        }
    }
}