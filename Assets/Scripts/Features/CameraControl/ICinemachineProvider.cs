using Cinemachine;

namespace Features.CameraControl
{
    public interface ICinemachineProvider
    {
        public CinemachineVirtualCamera Camera { get; }
    }
}