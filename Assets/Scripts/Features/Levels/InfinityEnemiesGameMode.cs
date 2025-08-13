using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Features.Enemy;
using Features.Player.Stats;
using Features.Score;
using Infrastructure.Factory;
using Infrastructure.Services.StaticDataService;
using StaticData;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Levels
{
    public class InfinityEnemiesGameMode : IInitializable, IDisposable, IExperienceScoreHolder
    {
        private const int ScoreStep = 1;
        public ReactiveProperty<int> ExperienceCounter { get; private set; } = new();
        
        private readonly IEnemyFactory _enemyFactory;
        private readonly LevelStaticData _staticData;
        private readonly IEnemyStaticDataProvider _enemiesDataProvider;

        private Dictionary<EnemyModel, IDisposable> enemies;
        private CancellationTokenSource _cts;

        public InfinityEnemiesGameMode(IEnemyFactory enemyFactory, LevelStaticData staticData, IEnemyStaticDataProvider enemiesDataProvider)
        {
            _enemyFactory = enemyFactory;
            _staticData = staticData;
            _enemiesDataProvider = enemiesDataProvider;
            
            enemies = new();
            _cts = new();
            ExperienceCounter = new ReactiveProperty<int>(0);
        }
        
        public void Initialize()
        {
            foreach (var spawnerData in _staticData.EnemySpawners)
            {
                var enemyConfig = _enemiesDataProvider.EnemyConfigs.FirstOrDefault(x => x.EnemyType == spawnerData.EnemyType);
                if (enemyConfig != null)
                {
                    SpawnEnemy(enemyConfig, spawnerData);
                }
            }
        }

        private void SpawnEnemy(EnemyConfig enemyConfig, EnemySpawnerStaticData spawnerData)
        {
            List<ICharacterStat> enemyStats = new List<ICharacterStat>();
            foreach (var config in enemyConfig.Stats)
            {
                var statModel = new CharacterStatModel(config);
                enemyStats.Add(statModel);
            }
            
            var model = new EnemyModel(enemyConfig.EnemyType, enemyStats);
            _enemyFactory.CreateEnemy(model, spawnerData.Position);

            var disposable = model.IsDeath.Subscribe(isDead =>
            {
                if (isDead)
                {
                    enemies[model].Dispose();
                    enemies.Remove(model);
                    
                    ExperienceCounter.Value += ScoreStep;
                    
                    SpawnNewEnemy(enemyConfig, spawnerData).Forget();
                }
            });
                    
            enemies.Add(model, disposable);
        }

        private async UniTask SpawnNewEnemy(EnemyConfig enemyConfig, EnemySpawnerStaticData spawnerData)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: _cts.Token);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Token was cancelled");
                return;
            }

            SpawnEnemy(enemyConfig, spawnerData);
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts = null;
            
            foreach (var pair in enemies)
            {
                pair.Value.Dispose();
            }
            enemies.Clear();
        }

    }
}