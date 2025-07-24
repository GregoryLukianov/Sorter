using System.Collections.Generic;
using Game;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemyCatalog", fileName = "EnemyCatalog")]
    public class EnemyCatalogSO: ScriptableObject
    {
        public List<EnemySO> Enemies;


        public EnemySO GetEnemy(EnemyType type) => Enemies.Find(enemy => enemy.Type == type);
    }
}