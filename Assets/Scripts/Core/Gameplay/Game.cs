using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Shapes.Factories.Base.Factories;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core.Gameplay
{
    public class Game: MonoBehaviour
    {
        [SerializeField] private float _startTime;
        [SerializeField] private List<Path> _paths;
        
        [Inject] private EnemyCatalogSO _enemyCatalog;
        [Inject] private LevelSettings _settings;
        [Inject] private EnemiesFactory _enemiesFactory;

        private int _numberOfFigures;
        private int _numberOfFiguresLeft;
        private float _spawnTime;
        private Coroutine _gameRoutine;
        private int _enemyCount => _enemyCatalog.Enemies.Count;
        

        private IEnumerator Start()
        {
            InitializeSettingsValues();
            
            yield return new WaitForSeconds(_startTime);
            _gameRoutine = StartCoroutine(GameRoutine());
        }

        private void InitializeSettingsValues()
        {
            _numberOfFigures = (int)Random.Range(_settings.MinNumberOfFigures, _settings.MaxNumberOfFigures);
            _numberOfFiguresLeft = _numberOfFigures;
            
            _spawnTime = Random.Range(_settings.MinSpawnTime, _settings.MaxSpawnTime);
        }

        private IEnumerator GameRoutine()
        {
            while (_numberOfFiguresLeft != 0)
            {
                var path = _paths[Random.Range(0, 3)];
                var enemySo = _enemyCatalog.Enemies[Random.Range(0, _enemyCount)];
                var enemySpeed = Random.Range(_settings.MinEnemySpeed, _settings.MaxEnemySpeed);
                
                _enemiesFactory.Create(enemySo, path.transform, enemySpeed, path.StartPoint.position,
                    path.EndPoint.position);
                _numberOfFiguresLeft -= 1;

                yield return new WaitForSeconds(_spawnTime);
            }
        }
    }
}