using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Boot
{
    public class MockAuthorizationService : IAuthService, IInitializable,  IDisposable
    {
        private const int MOCK_AWAIT_TIME = 2;
        
        public ReactiveCommand<bool> IsAuthComplete { get; } = new();
        
        private CancellationTokenSource _cts;
        private CancellationToken _token;
        
        public void Initialize()
        {
            _cts = new CancellationTokenSource();
            _token = _cts.Token;
            
            UserAuthentification(_token).Forget();
        }

        private async UniTaskVoid UserAuthentification(CancellationToken token)
        {
            try
            {
                await MockAuth(token);
                IsAuthComplete.Execute(true);
            }
            catch (Exception e)
            {
                Debug.LogError($"[UserAuthentification] Can't authentify user: {e.Message}");
                IsAuthComplete.Execute(false);
            }
        }

        private async UniTask MockAuth(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(MOCK_AWAIT_TIME), cancellationToken: token);
        }

        public void Dispose()
        {
            _cts.Cancel();
        }
    }
}