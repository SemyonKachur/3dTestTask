using UI.MainMenu;
using UniRx;

namespace Infrastructure.Services.InputService
{
    public interface IWindowOpenRequest
    {
        public ReactiveCommand<WindowType> OnShowWindow { get; }
    }
}