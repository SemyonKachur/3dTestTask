using UnityEngine;

namespace Features.Player.View
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public CharacterController CharacterController => _characterController;
        public Animator Animator => _animator;
        public Transform ShootPoint => _shootPoint;
        
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _shootPoint;
    }
}