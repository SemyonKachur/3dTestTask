using UniRx;

namespace Features.Score
{
    public interface IExperienceScoreHolder
    {
        public ReactiveProperty<int> ExperienceCounter { get; }
    }
}