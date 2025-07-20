using Core.Gameplay;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Enemy", fileName = "Enemy")]
    public class EnemySO: ScriptableObject
    {
        public EnemyType Type;
        public Enemy Prefab;
    }
}