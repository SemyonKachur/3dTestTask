using System;
using System.Collections.Generic;
using System.Linq;
using Features.Inventory;
using Features.Player.Stats;
using Infrastructure.Services.SaveService;
using UniRx;
using UnityEngine;

namespace Features.Player.Model
{
    public class PlayerModel : IPlayerModel, IDisposable
    {
        public List<ICharacterStat> Stats { get; private set; } = new();
        public List<IItem> Items { get; private set; } = new();
        public ReactiveProperty<bool> IsDead { get; } = new(false);

        private ICharacterStat _healthStat;
        private CompositeDisposable _disposables = new();

        public void LoadProgress(PlayerProgress progress)
        {
            Stats.Clear();
            Items.Clear();

            Stats = progress.CharacterStats;
            Items = progress.InventoryItems;
            
            _healthStat = Stats.FirstOrDefault(x => x.Id == CharacterStatTypeId.Health);
            if (_healthStat != null)
            {
                _healthStat.CurrentValue
                    .Subscribe(x => IsDead.Value = x <= 0)
                    .AddTo(_disposables);
            }
        }

        public void LoadPlayerStats(List<ICharacterStat> stats)
        {
            if (Stats.Count != 0)
            {
                Stats.Clear();
            }
            
            Stats = stats;
            _healthStat = Stats.FirstOrDefault(x => x.Id == CharacterStatTypeId.Health);
            if (_healthStat != null)
            {
                _healthStat.CurrentValue
                    .Subscribe(x => IsDead.Value = x <= 0)
                    .AddTo(_disposables);
            }
        }
        
        public void LoadPlayerItems(List<IItem> items)
        {
            if (Items.Count != 0)
            {
                Items.Clear();
            }
            Items = items;
        }

        public void AddPlayerStat(ICharacterStat stat)
        {
            Stats.Add(stat);
        }

        public void RemovePlayerStat(CharacterStatTypeId statId)
        {
            var targetStat = Stats.FirstOrDefault(x => x.Id == statId);
            if (targetStat != null)
            {
                Stats.Remove(targetStat);
            }
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}