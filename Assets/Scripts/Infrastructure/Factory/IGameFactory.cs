using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Progress;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        List<ISavedProgress> ProgressWriters { get; }
        List<ISavedProgressReader> ProgressReaders { get; set; }
        void Cleanup();
        void WarmUp();
        UniTask<GameObject> CreateHud();
    }
}