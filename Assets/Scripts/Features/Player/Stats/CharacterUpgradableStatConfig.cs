using UniRx;
using UnityEngine;

namespace Features.Player.Stats
{
    [CreateAssetMenu(fileName = nameof(CharacterUpgradableStatConfig),  menuName = "Stats/" + nameof(CharacterUpgradableStatConfig))]
    public class CharacterUpgradableStatConfig : CharacterStatConfig, IUpgradableCharacterStat
    {
        [field: SerializeField] public ReactiveProperty<int> CurrentLevel { get; private set; }
        [field: SerializeField] public UpgradeType UpgradeType { get; private set; }
        [field: SerializeField] public int MaxLevel { get; private set; }
        [field: SerializeField] public float AdditionalEffectValue { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public bool IsMaxLevel { get; private set; }
    }
}