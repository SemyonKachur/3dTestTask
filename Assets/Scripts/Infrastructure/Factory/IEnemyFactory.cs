using Cysharp.Threading.Tasks;
using Features.Enemy;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IEnemyFactory
    {
        public UniTask<IEnemyView> CreateEnemy(IEnemyModel enemy, Vector3 position);
    }
}