using Cysharp.Threading.Tasks;

namespace Infrastructure.Services.SaveService
{
    public interface ISaveLoadService : IService
    {
        UniTask SaveProgress();
        UniTask<PlayerProgress> LoadProgress();
    }
}