using UnityEngine;

namespace Features.Player.View
{
    public interface IPlayerView
    {
        public Transform PlayerRoot { get; }
        public Transform CameraRoot { get; }
        public Animator Animator { get; }
        public Transform ShootPoint { get; }

        public void AttackPerformed();
    }
}