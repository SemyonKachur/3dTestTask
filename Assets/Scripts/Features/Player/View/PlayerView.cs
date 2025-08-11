using UnityEngine;

namespace Features.Player.View
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public Transform PlayerRoot => root;
        public Transform CameraRoot => _cameraRoot;
        public Animator Animator => _animator;
        public Transform ShootPoint => _shootPoint;
        
        [SerializeField] private Transform root;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _cameraRoot;
    }
}