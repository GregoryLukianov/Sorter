using Events;
using Events.Handlers;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private EnemyType _requiredShape;
        
        private Coroutine _locatorRoutine;

        [Inject] private EventBus _eventBus;
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.GetComponent<Enemy>();
            if(!enemy)
                return;

            if (enemy.Type == _requiredShape)
                enemy.MoveToTarget(transform.position, true);
            else
            {
                enemy.Explode();
                _eventBus.RaiseEvent<IGetDamageHandler>(h=>h.HandleGetDamage());
            }
        }
    }
}