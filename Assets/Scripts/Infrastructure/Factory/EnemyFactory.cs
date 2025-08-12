using System.Linq;
using Cysharp.Threading.Tasks;
using Features.Enemy;
using Infrastructure.Services.ContentProvider;
using StaticData;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly LevelStaticData _levelData;
        private readonly IContentProvider _contentProvider;

        public EnemyFactory(LevelStaticData levelData, IContentProvider contentProvider)
        {
            _levelData = levelData;
            _contentProvider = contentProvider;
        }

        public async UniTask<IEnemyView> CreateEnemy(IEnemyModel enemy)
        {
            var enemyData = _levelData.EnemySpawners.First(x => x.EnemyType == enemy.EnemyType);
            var prefab = await _contentProvider.Load<EnemyView>(enemy.ToString());
            var enemyView = GameObject.Instantiate(prefab, enemyData.Position, Quaternion.identity);
             
            //TODO: добавить компоненты пресследования
            return enemyView;
        }
    }
}