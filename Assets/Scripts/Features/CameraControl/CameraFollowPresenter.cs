using System;
using Features.Player.View;
using Infrastructure.Utils;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.CameraControl
{
    public class CameraFollowPresenter : IInitializable, IDisposable
    {
        private readonly IPlayerView _playerView;
        private readonly ICameraProvider _cameraProvider;
        private readonly CompositeDisposable _disposable;
        
        private readonly Vector3 _offset = new Vector3(0f, 0.8f, 0.5f); 

        private Vector3 _currentVelocity;

        public CameraFollowPresenter(IPlayerView playerView, ICameraProvider cameraProvider)
        {
            _playerView = playerView;
            _cameraProvider = cameraProvider;
            _disposable = new CompositeDisposable();
        }

        public void Initialize()
        {
            Observable.EveryLateUpdate()
                .Subscribe(_ => UpdateCameraPosition())
                .AddTo(_disposable);
            
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            Vector3 targetPosition = _playerView.CharacterController.transform.position + 
                                     _playerView.CharacterController.transform.TransformDirection(_offset);

            _cameraProvider.Camera.transform.position = targetPosition;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}