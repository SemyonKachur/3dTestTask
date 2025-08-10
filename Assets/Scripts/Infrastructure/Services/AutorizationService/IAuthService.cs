using Infrastructure.Services.InputService;
using UniRx;

namespace Infrastructure.Services.Boot
{
    public interface IAuthService : IService
    {
        ReactiveCommand<bool> IsAuthComplete { get; }
    }
}