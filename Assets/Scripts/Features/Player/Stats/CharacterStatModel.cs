using System;
using UniRx;

namespace Features.Player.Stats
{
    [Serializable]
    public class CharacterStatModel : ICharacterStat
    {
        public CharacterStatTypeId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float MaxValue { get; set; }
        public ReactiveProperty<float> CurrentValue { get; set; } = new();
        public float DefaultValue { get; set; }

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

        public CharacterStatModel(CharacterStatConfig config)
        {
            Id = config.Id;
            Name = config.Name;
            Description = config.Description;
            MaxValue = config.MaxValue;
            CurrentValue.Value = config.CurrentValue.Value;
            DefaultValue = config.DefaultValue;
        }
        
        public ICharacterStat Clone()
        {
            return new CharacterStatModel()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                MaxValue = MaxValue,
                CurrentValue = new ReactiveProperty<float>(CurrentValue.Value),
                DefaultValue = DefaultValue
            };
        }

    }
}