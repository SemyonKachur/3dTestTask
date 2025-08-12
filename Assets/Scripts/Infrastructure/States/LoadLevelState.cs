using Cysharp.Threading.Tasks;
using Infrastructure.Factory;
using Infrastructure.Services.SceneLoader;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
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

        private void OnLoaded()
        {
           _stateMachine.Enter<GameLoopState>();
        }
    }
}