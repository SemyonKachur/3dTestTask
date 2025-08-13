using System;
using UnityEngine;

namespace Infrastructure.Services.Lifecycle
{
    public class ApplicationLifecycleService : MonoBehaviour
    {
        public event Action<bool> OnApplicationPauseEvent;
        public event Action<bool> OnApplicationFocusEvent;
        public event Action OnApplicationQuitEvent;

        private void OnApplicationFocus(bool isFocused)
        {
            OnApplicationFocusEvent?.Invoke(isFocused);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            OnApplicationPauseEvent?.Invoke(pauseStatus);
        }
        
        private void OnApplicationQuit()
        {
            OnApplicationQuitEvent?.Invoke();
        }
    }
}