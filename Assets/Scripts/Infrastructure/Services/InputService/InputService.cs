using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Infrastructure.Services.InputService
{
    public class InputService : IInputService, IInitializable, IDisposable
    {
        public ReactiveProperty<bool> IsFire => _isFire;

        public  ReactiveProperty<Vector2> Axis => _move;
        
        private PlayerInput _input;
        private ReactiveProperty<Vector2> _move = new(Vector2.zero);
        private ReactiveProperty<bool> _isFire = new(false);
        
        public void Initialize()
        {
            _input = new PlayerInput();
            EnablePlayerInput(true);
            
            _input.Player.Move.performed += Move;
            _input.Player.Move.canceled += StopMove;
            
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
                
                _input.Player.Fire.performed -= Fire;
                _input.Player.Fire.canceled -= StopFire;
                
                _input.Dispose();
            }
        }
    }
}