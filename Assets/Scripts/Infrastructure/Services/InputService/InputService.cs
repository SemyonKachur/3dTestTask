using System;
using Features.Player.Stats;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Infrastructure.Services.InputService
{
    public class InputService : IInputService, IInitializable, IDisposable
    {
        public Vector2 Move => _move;
        private Vector2 _move;
        
        public Vector2 Look => _look;
        private Vector2 _look;
        public bool IsFire => _isFire;
        private bool _isFire;
        
        public float RotateSpeed => _rotationSpeed;
        private float _rotationSpeed = 10;
        
        private PlayerInput _input;
        private ICharacterStat _moveStat;
        
        public void Initialize()
        {
            _input = new PlayerInput();
            EnablePlayerInput(true);
            
            _input.Player.Move.performed += SetMove;
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

        public void MoveInput(Vector2 newMoveDirection)
        {
            _move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            _look = newLookDirection;
        }

        public void FireInput(bool isFire)
        {
            _isFire = isFire;
        }

        private void Fire(InputAction.CallbackContext ctx) => 
            _isFire = true;
        
        private void StopFire(InputAction.CallbackContext obj) => 
            _isFire = false;
        
        private void Rotate(InputAction.CallbackContext ctx) =>
            _look = ctx.ReadValue<Vector2>();
        private void StopRotate(InputAction.CallbackContext ctx) =>
            _look = Vector2.zero;

        private void SetMove(InputAction.CallbackContext ctx) => 
            _move = ctx.ReadValue<Vector2>();

        private void StopMove(InputAction.CallbackContext ctx) => 
            _move = Vector2.zero;
        
        

        public void Dispose()
        {
            if (_input != null)
            {
                _input.Player.Move.performed -= SetMove;
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