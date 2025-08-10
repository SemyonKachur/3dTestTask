using UniRx;

namespace Features.Player.Stats
{
    public interface ICharacterStat
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public float MaxValue { get; }
        public ReactiveProperty<float> CurrentValue { get; }
        public float DefaultValue { get; }
    }
}