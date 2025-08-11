using System.Collections.Generic;
using Features.Inventory;
using Features.Player.Stats;
using Infrastructure.Services.SaveService;
using UniRx;

namespace Features.Player.Model
{
    public interface IPlayerModel
    {
        public List<ICharacterStat> Stats { get; }
        
        public List<IItem> Items { get; }
        
        public ReactiveProperty<bool> IsDead { get; }

        public void LoadProgress(PlayerProgress progress);

        public void AddPlayerStat(ICharacterStat stat);

        public void RemovePlayerStat(CharacterStatTypeId statId);
    }
}