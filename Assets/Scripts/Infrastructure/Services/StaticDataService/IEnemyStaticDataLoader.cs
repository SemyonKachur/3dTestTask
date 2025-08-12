using System.Collections.Generic;
using Features.Enemy;

namespace Infrastructure.Services.StaticDataService
{
    public interface IEnemyStaticDataLoader : IStaticDataLoader
    {
        public List<EnemyConfig> EnemyConfigs { get; }
    }
}