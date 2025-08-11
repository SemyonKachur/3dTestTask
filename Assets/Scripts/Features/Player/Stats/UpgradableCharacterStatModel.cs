using System;
using UniRx;

namespace Features.Player.Stats
{
    [Serializable]
    public class UpgradableCharacterStatModel : CharacterStatModel, IUpgradableCharacterStat
    {
        public ReactiveProperty<int> CurrentLevel { get; private set; } = new(0);
        public UpgradeType UpgradeType { get; private set; }
        public int MaxLevel { get; private set; }
        public float AdditionalEffectValue { get; private set; }
        public int Cost { get; private set; }
        public bool IsMaxLevel => CurrentLevel.Value >= MaxLevel;

        public UpgradableCharacterStatModel() : base(CharacterStatTypeId.None, "","",0,0,0)
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