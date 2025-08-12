using System.Collections.Generic;
using Features.Enemy;

namespace Infrastructure.Services.StaticDataService
{
    public interface IEnemyStaticDataProvider : IStaticDataLoader
    {
        public List<EnemyConfig> EnemyConfigs { get; }
    }
}