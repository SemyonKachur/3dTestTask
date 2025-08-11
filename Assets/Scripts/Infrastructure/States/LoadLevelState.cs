using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Features.Player.Model;
using Infrastructure.Factory;
using Infrastructure.Services.Progress;
using Infrastructure.Services.SceneLoader;
using Infrastructure.Services.StaticDataService;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;
        private readonly IStaticDataService _staticData;
        private readonly IUIFactory _uiFactory;
        private readonly IPlayerModel _playerModel;

        public LoadLevelState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory, 
            IProgressService progressService, IStaticDataService staticDataService, IUIFactory uiFactory, IPlayerModel playerModel)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticDataService;
            _uiFactory = uiFactory;
            _playerModel = playerModel;
        }

        public async UniTask Enter(string sceneName)
        {
            _gameFactory.Cleanup();
            _gameFactory.WarmUp();
            await _sceneLoader.LoadSceneAsync(sceneName, onLoaded: OnLoaded);
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }

        private async void OnLoaded()
        {
            await InitUIRoot();
            await InitGameWorld();
            InformProgressReaders();

           _stateMachine.Enter<GameLoopState>();
        }

        private async UniTask InitUIRoot() => 
            await _uiFactory.CreateUIRoot();

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private UniTask InitGameWorld()
        {
            //TODO: init factories and spawn world
            return UniTask.CompletedTask;
        }
    }
}