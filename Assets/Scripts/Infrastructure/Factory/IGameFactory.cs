using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Progress;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        void Cleanup();
        void WarmUp();
        UniTask<GameObject> CreateHud();
    }
}