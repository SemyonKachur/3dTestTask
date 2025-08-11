using System;
using UniRx;

namespace Features.Player.Stats
{
    [Serializable]
    public class UpgradableCharacterStatModel : CharacterStatModel, IUpgradableCharacterStat
    {
        public ReactiveProperty<int> CurrentLevel { get;  set; } = new(0);
        public UpgradeType UpgradeType { get; set; }
        public int MaxLevel { get; set; }
        public float AdditionalEffectValue { get; set; }
        public int Cost { get; set; }
        public bool IsMaxLevel => CurrentLevel.Value >= MaxLevel;

        public UpgradableCharacterStatModel()
        {
        }
        
        public UpgradableCharacterStatModel(CharacterStatTypeId id, string name, string description, float maxValue,  
            float? currentValue, float defaultValue, UpgradeType upgradeType,
            int maxLevel, float additionalEffectValue, int cost) 
            : base(id, name, description, maxValue, currentValue, defaultValue)
        {
            CurrentLevel = new ReactiveProperty<int>(1);
            UpgradeType = upgradeType;
            MaxLevel = maxLevel;
            AdditionalEffectValue = additionalEffectValue;
            Cost = cost;
        }
    }
}