using UnityEngine;

namespace Infrastructure.Services.TimeService
{
    public class TimeService : ITimeService
    {
        public bool IsPause => _isPause;
        private bool _isPause = false;

        public void SetPause(bool isPause)
        {
            Time.timeScale = isPause ? 0 : 1;
            _isPause = isPause;
        }
    }
}