using System;
using UI.MainMenu;
using UniRx;
using Zenject;

namespace UI.UpgradeWindow
{
    public class UpgradeWindowPresenter : IInitializable, IDisposable, IWindowPresenter
    {
        public ReactiveCommand<WindowType> OnShowWindow { get; } = new();
        public WindowType WindowType => WindowType.UpgradeWindow;

        public void Show()
        {
        }

        public void Hide()
        {
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}