namespace Infrastructure.Services.TimeService
{
    public interface ITimeService
    {
        public bool IsPause { get; }
        
        public void SetPause(bool isPause);
    }
}