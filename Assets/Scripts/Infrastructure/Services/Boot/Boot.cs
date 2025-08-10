using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.SceneLoader;
using UniRx;
using Zenject;

namespace Infrastructure.Services.Boot
{
    public class Boot : IInitializable, IDisposable
    {
        private const string GAME_SCENE_NAME = "Game";

        private readonly IAuthService _authService;
        private readonly ISceneLoader _sceneLoader;
        private readonly CompositeDisposable _disposables;

        public Boot(IAuthService authService, ISceneLoader sceneLoader)
        {
            _authService = authService;
            _sceneLoader = sceneLoader;

            _disposables = new();
        }
        
        public void Initialize()
        {
            SubscribeAuth();
        }

        private void SubscribeAuth()
        {
            _authService.IsAuthComplete
                .Subscribe(async x => StartGame(x).Forget())
                .AddTo(_disposables);
        }

        private async UniTask StartGame(bool isAuthComplete)
        {
            if (isAuthComplete)
            {
                await _sceneLoader.LoadSceneAsync(GAME_SCENE_NAME);
            }
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}