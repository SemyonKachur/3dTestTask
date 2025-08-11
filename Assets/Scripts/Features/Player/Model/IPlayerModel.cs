using System.Collections.Generic;
using Features.Player.Stats;
using UniRx;

namespace Features.Player.Model
{
    public interface IPlayerModel
    {
        public List<ICharacterStat> Stats { get; }
        
        public ReactiveProperty<bool> IsDead { get; }

        public void LoadPlayerStats(List<ICharacterStat> stats);

        public void AddPlayerStat(ICharacterStat stat);

        public void RemovePlayerStat(CharacterStatTypeId statId);
    }
}