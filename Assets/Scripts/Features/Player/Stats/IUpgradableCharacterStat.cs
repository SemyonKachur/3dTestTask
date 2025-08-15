using UniRx;

namespace Features.Player.Stats
{
    public interface IUpgradableCharacterStat : ICharacterStat
    {
        public ReactiveProperty<int> CurrentLevel { get; }
        public UpgradeType UpgradeType { get; }
        public int MaxLevel { get; }
        public float AdditionalEffectValue { get; }
        public int Cost { get; }
        public bool IsMaxLevel { get; }

        public void Upgrade();
        
        public new IUpgradableCharacterStat Clone();
    }
}