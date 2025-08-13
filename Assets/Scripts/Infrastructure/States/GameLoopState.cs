using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.SaveService;
using UniRx;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISaveLoadService _saveLoadService;

        private CompositeDisposable _disposable;

        public GameLoopState(GameStateMachine stateMachine, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
        }
        
        public UniTask Enter()
        {
            _disposable = new CompositeDisposable();
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _disposable.Dispose();
            return UniTask.CompletedTask;
        }
    }
}