using System;
using System.Linq;
using Features.Inventory;
using Features.Player.Model;
using Features.Player.Stats;
using Infrastructure.Services.InputService;
using UI.MainMenu;
using UniRx;
using Zenject;

namespace UI.GameWindow
{
    public class GameWindowPresenter : IInitializable, IDisposable, IWindowPresenter
    {
        public ReactiveCommand<WindowType> OnShowWindow { get; } = new();
        
        private readonly GameWindowView _view;
        private readonly IInputService _inputService;
        private readonly IPlayerModel _playerModel;

        private CompositeDisposable _disposables;

        public GameWindowPresenter(GameWindowView view, IInputService inputService, IPlayerModel playerModel)
        {
            _view = view;
            _inputService = inputService;
            _playerModel = playerModel;
        }
        
        public WindowType WindowType =>  WindowType.GameWindow;
        public void Show() => _view.Show();

        public void Hide() => _view.Hide();

        public void Initialize()
        {
            _disposables = new();

            IUpgradableCharacterStat health = _playerModel.Stats.First(x => x.Id == CharacterStatTypeId.Health) as IUpgradableCharacterStat;
            IItem experience = _playerModel.Items.First(x => x.ItemType == ItemType.Experience);
            _view.Construct(health, experience);
            
            _view.OnMenuButtonClicked.Subscribe(_ =>
            {
                OnShowWindow.Execute(WindowType.MainMenu);
            }).AddTo(_disposables);
            
            #if UNITY_ANDROID || UNITY_IOS
            _view.ShowMobileInput(true);
            
            _view.OnAttackClicked.Subscribe(isFire => _inputService.FireInput(isFire)).AddTo(_disposables);
            _view.OnJumpClicked.Subscribe(isJump => _inputService.JumpInput(isJump)).AddTo(_disposables);
            _view.OnRunClicked.Subscribe(isRun => _inputService.SprintInput(isRun)).AddTo(_disposables);
            
            Observable.EveryUpdate().Subscribe(x =>
            {
                _inputService.LookInput(_view.Look);
                _inputService.MoveInput(_view.Move);
            }).AddTo(_disposables);
            
            #else
            _view.ShowMobileInput(false);
            #endif
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}