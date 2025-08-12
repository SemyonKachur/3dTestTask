using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.ContentProvider
{
    public class ResourcesContentProvider : IContentProvider
    {
        public async UniTask<T> Load<T>(string path) where T : Object
        {
            var request = Resources.LoadAsync<T>(path);
            await request.ToUniTask();
            if (request.asset == null)
            {
                Debug.LogError($"Cant load asset from path: {path}");
            }
            return request.asset as T;
        }

        public async UniTask<T[]> LoadAll<T>(string path) where T : Object
        {
            var assets = Resources.LoadAll<T>(path);
            return await UniTask.FromResult(assets);
        }
    }
}