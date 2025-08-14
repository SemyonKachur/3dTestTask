using UniRx;

namespace UI.MainMenu
{
    public interface IWindowPresenter
    {
        public ReactiveCommand<WindowType> OnShowWindow { get; }
        
        public WindowType WindowType { get; }
        public void Show();
        public void Hide();
    }
}