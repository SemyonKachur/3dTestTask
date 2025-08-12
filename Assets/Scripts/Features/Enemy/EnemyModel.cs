using System.Collections.Generic;
using Features.Player.Stats;
using UniRx;

namespace Features.Enemy
{
    public class EnemyModel : IEnemyModel
    {
        public EnemyType EnemyType { get; private set; }
        public List<ICharacterStat> Stats { get; private set; }
        public ReactiveProperty<bool> IsDeath { get; private set; }

        public EnemyModel(EnemyType type, List<ICharacterStat> stats)
        {
            EnemyType =  type;
            Stats = stats;
            IsDeath = new ReactiveProperty<bool>(false);
        }
    }
}