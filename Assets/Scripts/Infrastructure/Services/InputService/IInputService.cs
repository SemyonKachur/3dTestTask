using UnityEngine;

namespace Infrastructure.Services.InputService
{
    public interface IInputService : IService
    {
        Vector2 Move { get; }
        Vector2 Look { get; }
        
        bool IsFire { get; }
        bool IsJump { get; }
        bool IsRun { get; }

        void EnablePlayerInput(bool isEnable);

        public void MoveInput(Vector2 newMoveDirection);

        public void LookInput(Vector2 newLookDirection);

        public void FireInput(bool isFire);

        public void JumpInput(bool newJumpState);
        
        public void SprintInput(bool newSprintState);
    }
}