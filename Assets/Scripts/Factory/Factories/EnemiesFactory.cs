using Events;
using Game;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Factory.Factories
{
    public class EnemiesFactory: CreatorMono<Enemy>
    {
        [Inject] private EnemyCatalogSO _enemyCatalog;
        
        
        public EnemiesFactory() { }
        
        public Enemy Create(EnemySO enemySO, Transform parent, float speed, Vector2 position, Vector2 target, EventBus eventBus)
        {
            Enemy enemy = base.Create(enemySO.Prefab, position, parent);
            enemy.Initialize(enemySO.Type, speed, target, eventBus);

            return enemy;
        }
    }
}