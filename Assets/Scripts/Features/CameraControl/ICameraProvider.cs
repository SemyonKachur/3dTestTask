using UnityEngine;

namespace Features.CameraControl
{
    public interface ICameraProvider
    {
        public Camera Camera { get; }
    }
}