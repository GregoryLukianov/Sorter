using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Events;
using Events.Handlers;
using Factory.Factories;
using ScriptableObjects;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game
{
    public class Gameplay : IGetDamageHandler, IAddScoreHandler, IEnemyDeathHandler
    {
        [Inject] private EnemyCatalogSO _enemyCatalog;
        [Inject] private LevelSettings _settings;
        [Inject] private EnemiesFactory _enemiesFactory;
        [Inject] private DiContainer _container;
        [Inject] private EventBus _eventBus;
        
        private List<Path> _paths;
        
        private int _numberOfEnemies;
        private int _numberOfEnemiesToSpawnLeft;
        private int _numberOfEnemiesAliveLeft;
        private float _spawnTime => Random.Range(_settings.MinSpawnTime, _settings.MaxSpawnTime);
        private int _healthPoints;
        private int _healthPointsLeft;
        private int _score;
        
        private int _enemyCount => _enemyCatalog.Enemies.Count;

        private CancellationTokenSource _cts;
        private UniTask _spawnTask;
        
        public void Init(List<Path> paths)
        {
            _eventBus.Subscribe(this);
            
            _paths = paths;
            
            InitializeValues();
            
            _eventBus.RaiseEvent<IHealthPointsChangeHandler>(h => h.HandleHealthPointsChange(_healthPointsLeft, _healthPoints));
            _eventBus.RaiseEvent<IScoreChangeHandler>(h => h.HandleScoreChange(_score));
        }

        public void StartGameplaySession()
        {
            _cts = new CancellationTokenSource();
            _spawnTask = StartSpawnRoutine(_cts.Token);
        }

        public void StopGameplay()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            _eventBus.Unsubscribe(this);
        }

        private void InitializeValues()
        {
            _numberOfEnemies = (int)Random.Range(_settings.MinNumberOfFigures, _settings.MaxNumberOfFigures);
            _numberOfEnemiesToSpawnLeft = _numberOfEnemies;
            _numberOfEnemiesAliveLeft = _numberOfEnemies;

            _healthPoints = _settings.HealthPoints;
            _healthPointsLeft = _healthPoints;
            _score = 0;
        }
        
        private async UniTask StartSpawnRoutine(CancellationToken token)
        {
            try
            {
                while (_numberOfEnemiesToSpawnLeft != 0 && !token.IsCancellationRequested)
                {
                    var path = _paths[Random.Range(0, 3)];
                    var enemySo = _enemyCatalog.Enemies[Random.Range(0, _enemyCount)];
                    var enemySpeed = Random.Range(_settings.MinEnemySpeed, _settings.MaxEnemySpeed);
                    
                    var enemy = _enemiesFactory.Create(enemySo, path.transform, enemySpeed, path.StartPoint.position,
                        path.EndPoint.position, _eventBus);
                    
                    _container.InjectGameObject(enemy.gameObject);
                    
                    _numberOfEnemiesToSpawnLeft -= 1;
                    await UniTask.Delay(System.TimeSpan.FromSeconds(_spawnTime), cancellationToken: token);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Spawn routine cancelled");
            }
        }
        
        private void ChickenDinner()
        {
            _eventBus.RaiseEvent<IVictoryHandler>(h=>h.HandleVictory(_score));
            StopGameplay();
        }

        public void HandleGetDamage()
        {
            _healthPointsLeft -= 1;
            _eventBus.RaiseEvent<IHealthPointsChangeHandler>(h => h.HandleHealthPointsChange(_healthPointsLeft, _healthPoints));

            if (_healthPointsLeft <= 0)
                GameOver();
        }

        private void GameOver()
        {
            _eventBus.RaiseEvent<IGameOverHandler>(h=>h.HandleGameOver());
            StopGameplay();
        }

        public void HandleAddScore()
        {
            _score += 1;
            _eventBus.RaiseEvent<IScoreChangeHandler>(h => h.HandleScoreChange(_score));
        }

        public void HandleEnemyDeath()
        {
            _numberOfEnemiesAliveLeft -= 1;
            
            if(_numberOfEnemiesAliveLeft>0)
                return;
            
            if (_healthPointsLeft > 0)
                ChickenDinner();
        }
    }
}