using System;
using System.Collections.Generic;
using System.Linq;
using Features.Enemy;
using Features.Player.Stats;
using Infrastructure.Factory;
using Infrastructure.Services.StaticDataService;
using StaticData;
using UniRx;
using Zenject;

namespace Features.Levels
{
    public class InfinityEnemiesGameMode : IInitializable, IDisposable
    {
        private readonly IEnemyFactory _enemyFactory;
        private readonly LevelStaticData _staticData;
        private readonly IEnemyStaticDataProvider _enemiesDataProvider;

        private Dictionary<EnemyModel, IDisposable> enemies;

        public InfinityEnemiesGameMode(IEnemyFactory enemyFactory, LevelStaticData staticData, IEnemyStaticDataProvider enemiesDataProvider)
        {
            _enemyFactory = enemyFactory;
            _staticData = staticData;
            _enemiesDataProvider = enemiesDataProvider;
            
            enemies = new();
        }
        
        public void Initialize()
        {
            foreach (var spawnerData in _staticData.EnemySpawners)
            {
                var enemyConfig = _enemiesDataProvider.EnemyConfigs.FirstOrDefault(x => x.EnemyType == spawnerData.EnemyType);
                if (enemyConfig != null)
                {
                    SpawnNewEnemy(enemyConfig, spawnerData);
                }
            }
        }

        private void SpawnNewEnemy(EnemyConfig enemyConfig, EnemySpawnerStaticData spawnerData)
        {
            var model = new EnemyModel(enemyConfig.EnemyType, enemyConfig.Stats.Cast<ICharacterStat>().ToList());
            _enemyFactory.CreateEnemy(model, spawnerData.Position);

            var disposable = model.IsDeath.Subscribe(isDead =>
            {
                if (isDead)
                {
                    enemies[model].Dispose();
                    enemies.Remove(model);
                            
                    SpawnNewEnemy(enemyConfig, spawnerData);
                }
            });
                    
            enemies.Add(model, disposable);
        }

        public void Dispose()
        {
            foreach (var pair in enemies)
            {
                pair.Value.Dispose();
            }
            enemies.Clear();
        }
    }
}