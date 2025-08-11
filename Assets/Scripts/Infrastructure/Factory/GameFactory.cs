using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Progress;
using Infrastructure.Services.SceneLoader;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private SceneLoader _sceneLoader;
        public List<ISavedProgress> ProgressWriters { get; } = new();
        public List<ISavedProgressReader> ProgressReaders { get; set; } = new();
        public void Cleanup()
        {
            //TODO: cleanup assets
        }

        public void WarmUp()
        {
            //TODO: preload assets
        }

        public UniTask<GameObject> CreateHud()
        {
            return new UniTask<GameObject>();
        }
    }
}