using UniRx;

namespace Infrastructure.Services.AutorizationService
{
    public interface IAuthService : IService
    {
        ReactiveCommand<bool> IsAuthComplete { get; }

        public void Initialize();
    }
}