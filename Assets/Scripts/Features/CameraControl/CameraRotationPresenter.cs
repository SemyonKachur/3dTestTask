using System;
using Features.Player.View;
using Infrastructure.Services.InputService;
using Infrastructure.Utils;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.CameraControl
{
    public class CameraRotationPresenter : IInitializable, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly IPlayerView _playerView;
        private readonly ICameraProvider _cameraProvider;
        private readonly CompositeDisposable _disposable;
        
        private readonly float _minVerticalAngle = -30f;
        private readonly float _maxVerticalAngle = 60f;

        private float _currentVerticalAngle; 

        public CameraRotationPresenter(IInputService inputService, IPlayerView playerView,
            ICameraProvider cameraProvider)
        {
            _inputService = inputService;
            _playerView = playerView;
            _cameraProvider = cameraProvider;
            _disposable = new CompositeDisposable();
        }

        public void Initialize()
        {
            _currentVerticalAngle = _cameraProvider.Camera.transform.eulerAngles.x;
            _currentVerticalAngle = NormalizeAngle(_currentVerticalAngle);
            
            Observable.EveryLateUpdate()
                .Where(_ => Mathf.Abs(_inputService.RotateAxisDelta.Value.y) > Constants.Epsilon)
                .Subscribe(_ => UpdateCameraRotation(_inputService.RotateAxisDelta.Value.y))
                .AddTo(_disposable);
            
            UpdateCameraRotation(0f);
        }

        private void UpdateCameraRotation(float verticalInput)
        {
            if (Mathf.Abs(verticalInput) > Constants.Epsilon)
            {
                float rotationDelta = verticalInput * Constants.RotateSpeed * Time.deltaTime;
                _currentVerticalAngle -= rotationDelta;
                _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, _minVerticalAngle, _maxVerticalAngle);
            }
            
            Vector3 lookAtPosition = _playerView.CharacterController.transform.position;
            Quaternion targetRotation = Quaternion.Euler(_currentVerticalAngle,
                _playerView.CharacterController.transform.eulerAngles.y, 0f);
            _cameraProvider.Camera.transform.rotation = targetRotation;
        }

        private float NormalizeAngle(float angle)
        {
            angle %= 360f;
            if (angle > 180f)
            {
                angle -= 360f;
            } 
            return angle;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}