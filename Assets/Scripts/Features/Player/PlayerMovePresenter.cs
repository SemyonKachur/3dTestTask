using System;
using Features.Player.Model;
using Features.Player.View;
using Infrastructure.Services.InputService;
using Infrastructure.Utils;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Player
{
    public class PlayerMovePresenter : IInitializable, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly IPlayerView _playerView;
        private readonly CompositeDisposable _disposable;

        public PlayerMovePresenter(IInputService inputService, IPlayerView playerView, IPlayerModel playerModel)
        {
            _inputService = inputService;
            _playerView = playerView;

            _disposable = new();
        }

        public void Initialize()
        {
            Observable.EveryLateUpdate()
                .Subscribe(_ =>
                {
                    MovePlayer(_inputService.MoveAxis.Value);
                    RotatePlayer(_inputService.RotateAxisDelta.Value);
                })
                .AddTo(_disposable);
        }

        private void RotatePlayer(Vector2 axisValue)
        {
            if (Mathf.Abs(axisValue.x) > Constants.Epsilon)
            {
                float rotationDelta = axisValue.x * _inputService.RotateSpeed * Time.deltaTime;
                Vector3 currentRotation = _playerView.CharacterController.transform.eulerAngles;
                
                float newRotationY = currentRotation.y + rotationDelta;
                _playerView.CharacterController.transform.rotation = Quaternion.Euler(0f, newRotationY, 0f);
            }
        }

        private void MovePlayer(Vector2 direction)
        {
            Vector3 movementVector = Vector3.zero;

            if (direction.sqrMagnitude > Constants.Epsilon)
            {
                direction.Normalize();
                
                Transform playerTransform = _playerView.CharacterController.transform;
                Vector3 forward = playerTransform.forward;
                Vector3 right = playerTransform.right; 
                
                forward.y = 0f;
                right.y = 0f;
                forward.Normalize();
                right.Normalize();
                
                movementVector = (forward * direction.y + right * direction.x);
            }
            
            _playerView.CharacterController.Move(movementVector * _inputService.MoveSpeed * Time.deltaTime);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}