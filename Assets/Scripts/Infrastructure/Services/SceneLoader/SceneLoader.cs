using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single,
            SceneSource source = SceneSource.BuildIn, Action onLoaded = null)
        {
            if (IsSceneLoaded(sceneName))
            {
                Debug.LogError($"[SceneLoader] Scene '{sceneName}' is already loaded.");
                onLoaded?.Invoke();
                return;
            }

            switch (source)
            {
                case SceneSource.BuildIn:
                {
                    await LoadBuildInScene(sceneName, mode, onLoaded);
                    break;
                }
                case SceneSource.Addressables:
                {
                    await LoadAddressableScene(sceneName, mode);
                    break;
                }
            }
        }

        private async UniTask LoadBuildInScene(string sceneName, LoadSceneMode mode, Action onLoaded = null)
        {
            try
            {
                AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneName, mode);
                if (waitNextScene == null)
                {
                    Debug.LogError($"[SceneLoader][LoadBuildInScene] Failed to load scene: '{sceneName}' via Unity.SceneManager.");
                    return;
                }

                await waitNextScene.ToUniTask();
                onLoaded?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError($"[SceneLoader][LoadBuildInScene] Load error: {e}");
            }
        }
        
        private async UniTask LoadAddressableScene(string sceneName, LoadSceneMode mode, Action onLoaded = null)
        {
            try
            {
                AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneName, mode);
                await handle.ToUniTask();
                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError($"[SceneLoader][LoadAddressableScene] Failed to load scene: '{sceneName}' via Addressable");
                    return;
                }
                
                onLoaded?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError($"[SceneLoader][LoadAdressableScene] Load error: {e}");
            }
        }

        public async UniTask UnloadSceneAsync(string sceneName)
        {
            if (!IsSceneLoaded(sceneName))
            {
                Debug.LogError($"[SceneLoader] Scene '{sceneName}' is not loaded.");
                return;
            }

            try
            {
                AsyncOperation waitUnload = SceneManager.UnloadSceneAsync(sceneName);
                if (waitUnload == null)
                {
                    Debug.LogError($"[SceneLoader] Failed to unload scene: '{sceneName}'");
                    return;
                }
                
                await waitUnload.ToUniTask();
            }
            catch (Exception e)
            {
                Debug.LogError($"[SceneLoader][UnloadSceneAsync] UnLoad error: {e}");
            }
        }
        
        private bool IsSceneLoaded(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var loadedScene = SceneManager.GetSceneAt(i);
                if (loadedScene.name == sceneName)
                    return true;
            }

            return false;
        }
    }
}