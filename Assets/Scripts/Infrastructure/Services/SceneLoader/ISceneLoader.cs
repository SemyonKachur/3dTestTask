using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.SceneLoader
{
    public interface ISceneLoader
    {
        UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, SceneSource source = SceneSource.BuildIn, Action onLoaded = null);
        
        UniTask UnloadSceneAsync(string sceneName);
    }
}