using UniRx;

namespace Features.Player.Stats
{
    public class CharacterStatModel : ICharacterStat
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public float MaxValue { get; private set; }
        public ReactiveProperty<float> CurrentValue { get; private set; } = new();
        public float DefaultValue { get; private set; }

        public CharacterStatModel(string id, string name, string description, float maxValue, float? currentValue, float defaultValue)
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