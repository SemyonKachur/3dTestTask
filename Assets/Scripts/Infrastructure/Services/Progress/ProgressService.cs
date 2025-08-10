using Infrastructure.Services.SaveService;

namespace Infrastructure.Services.Progress
{
    public class ProgressService : IProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}