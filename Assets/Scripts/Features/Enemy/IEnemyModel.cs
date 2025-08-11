using System.Collections.Generic;
using Features.Player.Stats;

namespace Features.Enemy
{
    public interface IEnemyModel
    {
        public EnemyType EnemyType { get; set; }
        public List<ICharacterStat> Stats { get; set; }
    }
}