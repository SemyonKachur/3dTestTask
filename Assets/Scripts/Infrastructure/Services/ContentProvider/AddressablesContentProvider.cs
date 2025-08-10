using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.ContentProvider
{
    public class AddressablesContentProvider : IContentProvider, IDisposable
    {
        private readonly List<AsyncOperationHandle> _handles = new List<AsyncOperationHandle>();

        public async UniTask<T> Load<T>(string path) where T : Object
        {
            var handle = Addressables.LoadAssetAsync<T>(path);
            _handles.Add(handle);

            await handle.ToUniTask();

            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new System.Exception($"Failed to load asset at path: {path}");

            return handle.Result;
        }

        public async UniTask<T[]> LoadAll<T>(string path) where T : Object
        {
            var handle = Addressables.LoadAssetsAsync<T>(path, null);
            _handles.Add(handle);

            await handle.ToUniTask();

            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new System.Exception($"Failed to load assets at path: {path}");

            return handle.Result.ToArray();
        }

        public void Dispose()
        {
            foreach (var handle in _handles)
            {
                if (handle.IsValid())
                    Addressables.Release(handle);
            }
            _handles.Clear();
        }
    }
}