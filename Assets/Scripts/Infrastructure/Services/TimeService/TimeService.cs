
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.TimeService
{
    public class TimeService : ITimeService, IInitializable
    {
        public bool IsPause => _isPause;
        private bool _isPause = false;

        public void SetPause(bool isPause)
        {
            Cursor.lockState = isPause ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isPause;
            
            Time.timeScale = isPause ? 0 : 1;
            _isPause = isPause;
        }

        public void Initialize()
        {
            SetPause(_isPause);
        }
    }
}