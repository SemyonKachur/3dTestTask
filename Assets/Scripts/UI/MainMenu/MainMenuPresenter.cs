using System;
using Infrastructure.Services.TimeService;
using UniRx;
using UnityEditor;
using Zenject;

namespace UI.MainMenu
{
    public class MainMenuPresenter : IInitializable, IDisposable, IWindowPresenter
    {
        public ReactiveCommand<WindowType> OnShowWindow { get; } = new();
        public WindowType WindowType => WindowType.MainMenu;
        
        private readonly ManMenuView _view;
        private readonly ITimeService _timeService;

        private CompositeDisposable _disposables = new CompositeDisposable();

        public MainMenuPresenter(ManMenuView view, ITimeService timeService)
        {
            _view = view;
            _timeService = timeService;
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void Initialize()
        {
            _view.OnContinueClick.Subscribe(x =>
            {
                OnShowWindow.Execute(WindowType.GameWindow);
            }).AddTo(_disposables);

            _view.OnUpgradeClick.Subscribe(x =>
            {
                Hide();
                OnShowWindow.Execute(WindowType.UpgradeWindow);
            }).AddTo(_disposables);
            
            _view.OnQuitClick.Subscribe(x =>
            {
                _timeService.SetPause(false);
#if  UNITY_EDITOR
                EditorApplication.isPlaying = false;  
#else
                Application.Quit();
#endif
            }).AddTo(_disposables);
        }

        public void Show() => _view.Show();

        public void Hide() => _view.Hide();
    }
}