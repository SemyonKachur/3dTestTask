using UnityEngine;

namespace Features.Player.View
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public Transform PlayerRoot => root;
        [SerializeField] private Transform root;
        
        public Transform CameraRoot => _cameraRoot;
        [SerializeField] private Transform _cameraRoot;
        
        public Animator Animator => _animator;
        [SerializeField] private Animator _animator;
        
        public Transform ShootPoint => _shootPoint;
        [SerializeField] private Transform _shootPoint;
        
        public virtual void AttackPerformed()
        {
            //TODO: add VFX or another addition view if needed    
        }
    }
}