using UniRx;
using UnityEngine;

namespace Infrastructure.Services.InputService
{
    public interface IInputService : IService
    {
        ReactiveProperty<Vector2> Axis { get; }
        
        ReactiveProperty<bool> IsFire { get; }
        
        void EnablePlayerInput(bool isEnable);
    }
}