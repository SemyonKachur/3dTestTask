using UniRx;
using UnityEngine;

namespace Infrastructure.Services.InputService
{
    public interface IInputService : IService
    {
        public float MoveSpeed { get; }
        public float RotateSpeed { get; }

        ReactiveProperty<Vector2> MoveAxis { get; }
        ReactiveProperty<Vector2> RotateAxisDelta { get; }
        
        ReactiveProperty<bool> IsFire { get; }
        
        void EnablePlayerInput(bool isEnable);
    }
}