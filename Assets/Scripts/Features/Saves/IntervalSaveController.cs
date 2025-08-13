using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.SaveService;
using UniRx;
using Zenject;

namespace Features.Saves
{
    public class IntervalSaveController : IInitializable, IDisposable
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly CompositeDisposable _disposable;

        public IntervalSaveController(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _disposable = new();
        }

        public void Initialize()
        {
            Observable.Interval(TimeSpan.FromSeconds(30))
                .Subscribe( async x => await SaveProgress(x))
                .AddTo(_disposable);
        }

        private async UniTask SaveProgress(long l)
        {
            await _saveLoadService.SaveProgress();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}