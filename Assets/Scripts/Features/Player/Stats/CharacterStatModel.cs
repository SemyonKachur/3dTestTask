using System;
using UniRx;

namespace Features.Player.Stats
{
    [Serializable]
    public class CharacterStatModel : ICharacterStat
    {
        public CharacterStatTypeId Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public float MaxValue { get; private set; }
        public ReactiveProperty<float> CurrentValue { get; private set; } = new();
        public float DefaultValue { get; private set; }

        public CharacterStatModel()
        {
        }

        public CharacterStatModel(CharacterStatTypeId id, string name, string description, float maxValue, float? currentValue, float defaultValue)
        {
            Id = id;
            Name = name;
            Description = description;
            MaxValue = maxValue;
            CurrentValue.Value = currentValue ?? defaultValue;
            DefaultValue = defaultValue;
        }
    }
}