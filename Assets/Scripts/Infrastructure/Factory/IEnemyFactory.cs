using Cysharp.Threading.Tasks;
using Features.Enemy;

namespace Infrastructure.Factory
{
    public interface IEnemyFactory
    {
        public UniTask<IEnemyView> CreateEnemy(IEnemyModel enemy);
    }
}