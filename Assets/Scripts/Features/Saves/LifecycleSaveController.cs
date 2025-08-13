using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Lifecycle;
using Infrastructure.Services.SaveService;
using Zenject;

namespace Features.Saves
{
    public class LifecycleSaveController : IInitializable, IDisposable
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly ApplicationLifecycleService _applicationLifecycleService;

        public LifecycleSaveController(ISaveLoadService saveLoadService,
            ApplicationLifecycleService applicationLifecycleService)
        {
            _saveLoadService = saveLoadService;
            _applicationLifecycleService = applicationLifecycleService;
        }

        public void Initialize()
        {
            _applicationLifecycleService.OnApplicationQuitEvent += SaveOnApplicationQuit;
            _applicationLifecycleService.OnApplicationFocusEvent += SaveOnApplicationFocus;
        }

        private void SaveOnApplicationFocus(bool isFocus)
        {
            if (!isFocus)
            {
                _saveLoadService.SaveProgress().Forget();
            }
        }

        private void SaveOnApplicationQuit()
        {
            _saveLoadService.SaveProgress().Forget();
        }

        public void Dispose()
        {
            _applicationLifecycleService.OnApplicationQuitEvent -= SaveOnApplicationQuit;
            _applicationLifecycleService.OnApplicationFocusEvent -= SaveOnApplicationFocus;
        }
    }
}