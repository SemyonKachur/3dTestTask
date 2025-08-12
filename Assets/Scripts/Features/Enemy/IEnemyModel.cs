using System.Collections.Generic;
using Features.Player.Stats;
using UniRx;

namespace Features.Enemy
{
    public interface IEnemyModel
    {
        public EnemyType EnemyType { get; }
        public List<ICharacterStat> Stats { get; }
        public ReactiveProperty<bool> IsDeath { get; }
    }
}