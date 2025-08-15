using UniRx;

namespace Features.Player.Stats
{
    public interface ICharacterStat
    {
        public CharacterStatTypeId Id { get; }
        public string Name { get; }
        public string Description { get; }
        public float MaxValue { get; set; }
        public ReactiveProperty<float> CurrentValue { get; }
        public float DefaultValue { get; }

        public ICharacterStat Clone();
    }
}