using System;
using UniRx;
using UnityEngine;

namespace Features.Player.Stats
{
    [Serializable]
    [CreateAssetMenu(fileName = nameof(CharacterStatConfig),  menuName = "Stats/" + nameof(CharacterStatConfig))]
    public class CharacterStatConfig : ScriptableObject, ICharacterStat
    {
        [field: SerializeField] public CharacterStatTypeId Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public float MaxValue { get; set; }
        [field: SerializeField] public float MinValue { get; private set; }
        [field: SerializeField] public ReactiveProperty<float> CurrentValue { get; private set; }
        [field: SerializeField] public float DefaultValue { get; private set; }
        
        public ICharacterStat Clone()
        {
            return CreateInstance<CharacterStatConfig>();
        }
    }
}