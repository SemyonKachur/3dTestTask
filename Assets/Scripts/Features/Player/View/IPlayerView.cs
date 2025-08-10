using UnityEngine;

namespace Features.Player.View
{
    public interface IPlayerView
    {
        public CharacterController CharacterController { get; }
        public Animator Animator { get; }
        public Transform ShootPoint { get; }
    }
}