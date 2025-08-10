using UniRx;

namespace Features.Player.Stats
{
    public class UpgradableCharacterStatModel : CharacterStatModel, IUpgradableCharacterStat
    {
        public ReactiveProperty<int> CurrentLevel { get; private set; } = new(0);
        public UpgradeType UpgradeType { get; private set; }
        public int MaxLevel { get; private set; }
        public float AdditionalEffectValue { get; private set; }
        public int Cost { get; private set; }
        public bool IsMaxLevel { get; private set; }
        
        public UpgradableCharacterStatModel(string id, string name, string description, float maxValue,  
            float? currentValue, float defaultValue, ReactiveProperty<int> currentLevel, UpgradeType upgradeType,
            int maxLevel, float additionalEffectValue, int cost, bool canUpgrade) 
            : base(id, name, description, maxValue, currentValue, defaultValue)
        {
            CurrentLevel = currentLevel;
            UpgradeType = upgradeType;
            MaxLevel = maxLevel;
            AdditionalEffectValue = additionalEffectValue;
            Cost = cost;
            IsMaxLevel = canUpgrade;
        }
    }
}