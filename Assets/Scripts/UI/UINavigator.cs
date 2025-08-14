using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.InputService;
using Infrastructure.Services.TimeService;
using UI.MainMenu;
using UniRx;
using Zenject;

namespace UI
{
    public class UINavigator : IInitializable, IDisposable
    {
        private readonly List<IWindowPresenter> _presenters;
        private readonly List<IWindowOpenRequest> _requestElements;
        private readonly ITimeService _timeService;
        
        private CompositeDisposable _disposables; 

        public UINavigator(List<IWindowPresenter> presenters,  List<IWindowOpenRequest> requestElements, ITimeService timeService)
        {
            _presenters = presenters;
            _requestElements = requestElements;
            _timeService = timeService;
        }
        
        public void Initialize()
        {
            _disposables = new();
            foreach (var presenter in _presenters)
            {
                presenter.OnShowWindow.Subscribe(Show).AddTo(_disposables);
            }

            foreach (var requestElement in _requestElements)
            {
                requestElement.OnShowWindow.Subscribe(Show).AddTo(_disposables);
            }
        }

        public void Show(WindowType windowType)
        {
            _presenters.ForEach(p => p.Hide());
            _presenters.FirstOrDefault(x => x.WindowType == windowType)?.Show();
            
            if(windowType == WindowType.MainMenu)
                _timeService.SetPause(true);
            if(windowType == WindowType.GameWindow)
                _timeService.SetPause(false);
        }
        
        public void Dispose() => _disposables.Dispose();
    }
}