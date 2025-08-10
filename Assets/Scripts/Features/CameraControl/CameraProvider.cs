using UnityEngine;

namespace Features.CameraControl
{
    public class CameraProvider : MonoBehaviour, ICameraProvider
    {
        public Camera Camera => _camera;
        
        [SerializeField] private Camera _camera;
    }
}