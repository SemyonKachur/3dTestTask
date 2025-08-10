using UnityEngine;

namespace Infrastructure.Services.InputService
{
    public abstract class InputService : IInputService
    {
        public Vector2 Axis { get; }
        public abstract bool IsAttackClicked();
    }
}