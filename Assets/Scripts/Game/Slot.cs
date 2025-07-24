using Events;
using Events.Handlers;
using UnityEngine;
using Zenject;

namespace Game
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
                enemy.Explode();
                _eventBus.RaiseEvent<IGetDamageHandler>(h=>h.HandleGetDamage());
            }
        }
    }
}