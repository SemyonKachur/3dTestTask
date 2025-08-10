using UniRx;
using UnityEngine;

namespace Features.Player.Stats
{
    [CreateAssetMenu(fileName = nameof(CharacterStatConfig),  menuName = "Stats/" + nameof(CharacterStatConfig))]
    public class CharacterStatConfig : ScriptableObject, ICharacterStat
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public float MaxValue { get; private set; }
        [field: SerializeField] public float MinValue { get; private set; }
        [field: SerializeField] public ReactiveProperty<float> CurrentValue { get; private set; }
        [field: SerializeField] public float DefaultValue { get; private set; }
    }
}