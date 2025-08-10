using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Infrastructure.Services.InputService
{
    public class InputService : IInputService, IInitializable, IDisposable
    {
        public ReactiveProperty<Vector2> RotateAxisDelta => _rotateAxisDelta;
        private ReactiveProperty<Vector2> _rotateAxisDelta = new (Vector2.zero);
        public ReactiveProperty<bool> IsFire => _isFire;
        private ReactiveProperty<bool> _isFire = new(false);

        public float MoveSpeed => _moveSpeed;
        public float RotateSpeed => _rotationSpeed;
        public  ReactiveProperty<Vector2> MoveAxis => _move;
        private ReactiveProperty<Vector2> _move = new(Vector2.zero);
        
        private PlayerInput _input;
        
        //TODO: вынести в игровые конфигурации для настройки через UI
        private float _moveSpeed = 5;
        public float _rotationSpeed = 10;
        
        public void Initialize()
        {
            _input = new PlayerInput();
            EnablePlayerInput(true);
            
            _input.Player.Move.performed += Move;
            _input.Player.Move.canceled += StopMove;
            
            _input.Player.Rotate.performed += Rotate;
            _input.Player.Rotate.canceled += StopRotate;
            
            _input.Player.Fire.performed += Fire;
            _input.Player.Fire.canceled += StopFire;
        }
        
        public void EnablePlayerInput(bool isEnable)
        {
            if (isEnable)
            {
                _input.Enable();
            }
            else
            {
                _input.Disable();
            }
        }

        private void Fire(InputAction.CallbackContext ctx) => 
            _isFire.Value = true;
        
        private void StopFire(InputAction.CallbackContext obj) => 
            _isFire.Value = false;
        
        private void Rotate(InputAction.CallbackContext ctx) =>
            _rotateAxisDelta.Value = ctx.ReadValue<Vector2>();
        private void StopRotate(InputAction.CallbackContext ctx) =>
            _rotateAxisDelta.Value = Vector2.zero;

        private void Move(InputAction.CallbackContext ctx) => 
            _move.Value = ctx.ReadValue<Vector2>();

        private void StopMove(InputAction.CallbackContext ctx) => 
            _move.Value = Vector2.zero;

        public void Dispose()
        {
            if (_input != null)
            {
                _input.Player.Move.performed -= Move;
                _input.Player.Move.canceled -= StopMove;
                
                _input.Player.Rotate.performed -= Rotate;
                _input.Player.Rotate.canceled -= StopRotate;
                
                _input.Player.Fire.performed -= Fire;
                _input.Player.Fire.canceled -= StopFire;
                
                _input.Dispose();
            }
        }
    }
}