using System;
using System.Collections;
using System.Collections.Generic;
using Core.Events;
using Core.Events.Handlers;
using ScriptableObjects;
using Shapes.Factories.Base.Factories;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core.Gameplay
{
    public class Game: MonoBehaviour, IHealthPointsHandler, IScoreHandler
    {
        [SerializeField] private float _startTime;
        [SerializeField] private List<Path> _paths;
        
        [Inject] private EnemyCatalogSO _enemyCatalog;
        [Inject] private LevelSettings _settings;
        [Inject] private EnemiesFactory _enemiesFactory;
        [Inject] private DiContainer _container;
        [Inject] private EventBus _eventBus;

        private int _numberOfFigures;
        private int _numberOfFiguresLeft;
        private float _spawnTime;
        private int _healthPoints;
        private int _healthPointsLeft;
        private int _score;
        
        private Coroutine _spawnRoutine;
        private int _enemyCount => _enemyCatalog.Enemies.Count;


        private void Awake()
        {
            _eventBus.Subscribe(this);
        }

        private IEnumerator Start()
        {
            InitializeSettingsValues();
            
            yield return new WaitForSeconds(_startTime);
            _spawnRoutine = StartCoroutine(SpawnRoutine());
        }

        private void InitializeSettingsValues()
        {
            _numberOfFigures = (int)Random.Range(_settings.MinNumberOfFigures, _settings.MaxNumberOfFigures);
            _numberOfFiguresLeft = _numberOfFigures;
            
            _spawnTime = Random.Range(_settings.MinSpawnTime, _settings.MaxSpawnTime);

            _healthPoints = _settings.HealthPoints;
            _healthPointsLeft = _healthPoints;
            _eventBus.RaiseEvent<IUpdateHealthPointsUIHandler>(h=>h.UpdateHealthPoints(_healthPointsLeft));

            _score = 0;
            
            _eventBus.RaiseEvent<IUpdateScoreUIHandler>(h=>h.UpdateScoreUI(_score));
        }

        private IEnumerator SpawnRoutine()
        {
            while (_numberOfFiguresLeft != 0)
            {
                var path = _paths[Random.Range(0, 3)];
                var enemySo = _enemyCatalog.Enemies[Random.Range(0, _enemyCount)];
                var enemySpeed = Random.Range(_settings.MinEnemySpeed, _settings.MaxEnemySpeed);
                
                var enemy = _enemiesFactory.Create(enemySo, path.transform, enemySpeed, path.StartPoint.position,
                    path.EndPoint.position);
                
                _container.InjectGameObject(enemy.gameObject);
                _numberOfFiguresLeft -= 1;

                yield return new WaitForSeconds(_spawnTime);
            }

            if (_healthPointsLeft > 0)
                ChickenDinner();
        }

        private void ChickenDinner()
        {
            Debug.Log("WinnerWinnerChickenDinner");
        }

        public void GetDamage()
        {
            _healthPointsLeft -= 1;
            _eventBus.RaiseEvent<IUpdateHealthPointsUIHandler>(h=>h.UpdateHealthPoints(_healthPointsLeft));

            if (_healthPointsLeft <= 0)
                GameOver();
        }

        private void GameOver()
        {
            Debug.Log("GameOver");
        }

        public void AddScore()
        {
            _score += 1;
            _eventBus.RaiseEvent<IUpdateScoreUIHandler>(h=>h.UpdateScoreUI(_score));
        }
    }
}