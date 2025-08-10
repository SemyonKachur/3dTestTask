using UniRx;
using UnityEngine;

namespace Infrastructure.Services.InputService
{
    public interface IInputService : IService
    {
        ReactiveProperty<Vector2> MoveAxis { get; }
        ReactiveProperty<Vector2> RotateAxisDelta { get; }
        
        ReactiveProperty<bool> IsFire { get; }
        
        void EnablePlayerInput(bool isEnable);
    }
}