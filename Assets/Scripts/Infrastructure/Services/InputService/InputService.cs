using System;
using System.Collections.Generic;
using UI.MainMenu;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Infrastructure.Services.InputService
{
    public class InputService : IInputService, IInitializable, IDisposable, IWindowOpenRequest
    {
        public ReactiveCommand<WindowType> OnShowWindow { get; } = new();
        
        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool IsFire { get; private set; }
        public bool IsJump { get; private set; }
        public bool IsRun { get; private set; }
        
        public bool IsPause { get; private set; }
        

        private PlayerInput _input;
        private readonly List<Action> _unbinders = new();

        public void Initialize()
        {
            _input = new PlayerInput();
            EnablePlayerInput(true);

            BindValue(_input.Player.Move, vector2 => Move = vector2);
            BindValue(_input.Player.Rotate, vector2 => Look = vector2);
            BindButton(_input.Player.Fire, boolValue => IsFire = boolValue);
            BindButton(_input.Player.Jump, boolValue => IsJump = boolValue);
            BindButton(_input.Player.Run, boolValue => IsRun = boolValue);
            BindButton(_input.Player.Pause, boolValue => 
            {
                if (boolValue)
                {
                    OnShowWindow.Execute(IsPause ? WindowType.GameWindow : WindowType.MainMenu);
                    IsPause = !IsPause;
                }
            });
        }

        private void BindValue(InputAction action, Action<Vector2> setter)
        {
            void OnPerformed(InputAction.CallbackContext ctx) => setter(ctx.ReadValue<Vector2>());
            void OnCanceled(InputAction.CallbackContext ctx) => setter(Vector2.zero);

            action.performed += OnPerformed;
            action.canceled += OnCanceled;

            _unbinders.Add(() =>
            {
                action.performed -= OnPerformed;
                action.canceled -= OnCanceled;
            });
        }

        private void BindButton(InputAction action, Action<bool> setter)
        {
            void OnPerformed(InputAction.CallbackContext _) => setter(true);
            void OnCanceled(InputAction.CallbackContext _) => setter(false);

            action.performed += OnPerformed;
            action.canceled += OnCanceled;

            _unbinders.Add(() =>
            {
                action.performed -= OnPerformed;
                action.canceled -= OnCanceled;
            });
        }
        
        public void MoveInput(Vector2 newMoveDirection) => Move = newMoveDirection;
        public void LookInput(Vector2 newLookDirection) => Look = newLookDirection;
        public void FireInput(bool isFire) => IsFire = isFire;
        public void JumpInput(bool newJumpState) => IsJump = newJumpState;
        public void SprintInput(bool newSprintState) => IsRun = newSprintState;

        public void EnablePlayerInput(bool isEnable)
        {
            if (isEnable) _input.Enable();
            else _input.Disable();
        }

        public void Dispose()
        {
            foreach (var unbind in _unbinders)
                unbind();
            _input.Dispose();
        }

    }
}