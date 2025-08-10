using Cysharp.Threading.Tasks;

namespace Infrastructure.Services.ContentProvider
{
    public interface IContentProvider
    {
        public UniTask<T> Load<T>(string path) where T : UnityEngine.Object;
        public UniTask<T[]> LoadAll<T>(string path) where T : UnityEngine.Object;
    }
}