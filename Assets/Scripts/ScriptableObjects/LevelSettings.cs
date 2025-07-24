using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/LevelSettings", fileName = "LevelSettings")]
    public class LevelSettings: ScriptableObject
    {
        [MinMaxSlider(0, 100)] [SerializeField] private Vector2 NumberOfFigures;
        public float MinNumberOfFigures => NumberOfFigures.x;
        public float MaxNumberOfFigures => NumberOfFigures.y;
        
        [MinMaxSlider(0, 5)] [SerializeField] private Vector2 SpawnTime;
        public float MinSpawnTime => SpawnTime.x;
        public float MaxSpawnTime => SpawnTime.y;
        
        [MinMaxSlider(0, 5)] [SerializeField] private Vector2 EnemySpeed;
        public float MinEnemySpeed => EnemySpeed.x;
        public float MaxEnemySpeed => EnemySpeed.y;

        public int HealthPoints;
    }
}