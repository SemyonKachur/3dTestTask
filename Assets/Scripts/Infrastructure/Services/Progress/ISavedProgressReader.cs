using Infrastructure.Services.SaveService;

namespace Infrastructure.Services.Progress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}