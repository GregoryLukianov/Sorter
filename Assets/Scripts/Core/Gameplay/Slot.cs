using System;
using System.Collections;
using Core.Events;
using Core.Events.Handlers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Gameplay
{
    public class Slot : MonoBehaviour
    {
        private Coroutine _locatorRoutine;
        public EnemyType requiredShape;

        [Inject] private EventBus _eventBus;
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.GetComponent<Enemy>();
            if(!enemy)
                return;

            if (enemy.Type == requiredShape)
                enemy.MoveToTarget(transform.position, true);
            else
            {
                Destroy(enemy.gameObject);
                _eventBus.RaiseEvent<IHealthPointsHandler>(h=>h.GetDamage());
            }
        }
    }
}