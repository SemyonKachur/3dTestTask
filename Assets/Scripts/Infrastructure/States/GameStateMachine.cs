using System;
using System.Collections.Generic;
using Features.Player.Model;
using Infrastructure.Factory;
using Infrastructure.Services.AutorizationService;
using Infrastructure.Services.Progress;
using Infrastructure.Services.SaveService;
using Infrastructure.Services.SceneLoader;
using Infrastructure.Services.StaticDataService;
using Zenject;

namespace Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPlayerStaticDataProvider _staticDataProvider;
        private readonly IAuthService _authService;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPlayerModel _playerModel;
        
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(DiContainer container)
        {
            _sceneLoader =  container.Resolve<ISceneLoader>();
            _progressService = container.Resolve<IProgressService>();
            _saveLoadService = container.Resolve<ISaveLoadService>();
            _staticDataProvider = container.Resolve<IPlayerStaticDataProvider>();
            _authService = container.Resolve<IAuthService>();
            _gameFactory = container.Resolve<IGameFactory>();
            _uiFactory = container.Resolve<IUIFactory>();
            _playerModel = container.Resolve<IPlayerModel>();
            
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, _sceneLoader, _authService),
                [typeof(LoadProgressState)] = new LoadProgressState(this, _progressService, _saveLoadService, _staticDataProvider, _playerModel),
                [typeof(LoadLevelState)] = new LoadLevelState(this,  _sceneLoader, _gameFactory),
                [typeof(GameLoopState)] = new GameLoopState(this, _saveLoadService)
            };
        }
    
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
      
            TState state = GetState<TState>();
            _activeState = state;
      
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}