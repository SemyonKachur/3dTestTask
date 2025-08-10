using UnityEngine;

namespace Infrastructure.Services.InputService
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool IsAttackClicked();
    }
}