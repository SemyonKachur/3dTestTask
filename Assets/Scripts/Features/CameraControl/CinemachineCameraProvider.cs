using Cinemachine;
using UnityEngine;

namespace Features.CameraControl
{
    public class CinemachineCameraProvider : MonoBehaviour, ICinemachineProvider
    {
        public CinemachineVirtualCamera Camera => _camera;
        [SerializeField] private CinemachineVirtualCamera _camera;
    }
}