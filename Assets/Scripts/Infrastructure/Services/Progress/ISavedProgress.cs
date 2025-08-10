using Infrastructure.Services.SaveService;

namespace Infrastructure.Services.Progress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}