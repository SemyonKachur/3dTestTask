using System;
using Zenject;

namespace Features.Score
{
    public interface IPlayerExperienceProvider : IInitializable, IDisposable
    {
        void AddToUserScore(int value);
    }
}