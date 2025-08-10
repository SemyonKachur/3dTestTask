using Infrastructure.Services.SaveService;

namespace Infrastructure.Services.Progress
{
    public interface IProgressService
    {
        PlayerProgress Progress { get; set; }
    }
}