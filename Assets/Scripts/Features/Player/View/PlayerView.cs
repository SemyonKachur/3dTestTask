using UnityEngine;

namespace Features.Player.View
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private CharacterController _characterController;
        
        public CharacterController CharacterController => _characterController;
    }
}