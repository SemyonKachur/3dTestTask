using System.Collections.Generic;
using Infrastructure.Services.Progress;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        List<ISavedProgress> ProgressWriters { get; }
    }
}