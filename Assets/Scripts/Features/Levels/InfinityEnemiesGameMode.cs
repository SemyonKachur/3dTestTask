using System;
using Infrastructure.Factory;
using StaticData;
using Zenject;

namespace Features.Levels
{
    public class InfinityEnemiesGameMode : IInitializable, IDisposable
    {
        private readonly IEnemyFactory _enemyFactory;
        private readonly LevelStaticData _staticData;

        public InfinityEnemiesGameMode(IEnemyFactory enemyFactory, LevelStaticData staticData)
        {
            _enemyFactory = enemyFactory;
            _staticData = staticData;
        }
        
        public void Initialize()
        {
            
        }
        
        public void Dispose()
        {
            
            
        }
    }
}