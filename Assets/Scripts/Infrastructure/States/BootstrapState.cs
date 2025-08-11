using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.AutorizationService;
using Infrastructure.Services.SceneLoader;
using UniRx;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IAuthService _authService;
        private CompositeDisposable _disposble;

        public BootstrapState(GameStateMachine stateMachine, ISceneLoader sceneLoader, IAuthService authService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _authService = authService;
            
            _disposble = new CompositeDisposable();
        }

        public async UniTask Enter()
        {
            _disposble = new CompositeDisposable();
            await _sceneLoader.LoadSceneAsync(Initial, onLoaded: StartAuth);
        }

        private void StartAuth()
        {
            _authService.Initialize();
            
            _authService.IsAuthComplete
                .Subscribe(AuthResultAction)
                .AddTo(_disposble);
        }

        private void AuthResultAction(bool isSuccess)
        {
            if (isSuccess)
            {
                _stateMachine.Enter<LoadProgressState>();
            }
            else
            {
                Debug.LogError("Auth failed. Please try again.");
                _authService.Initialize();
            }
        }

        public UniTask Exit()
        {
            _disposble.Dispose();
            return UniTask.CompletedTask;
        }
    }
}