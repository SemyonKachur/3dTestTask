using System;
using Features.Player.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.CameraControl
{
    public class CameraFollowPresenter : IInitializable, IDisposable
    {
        private readonly IPlayerView _playerView;
        private readonly ICinemachineProvider _cameraProvider;
        private readonly CompositeDisposable _disposable;

        private Vector3 _currentVelocity;

        public CameraFollowPresenter(IPlayerView playerView, ICinemachineProvider cameraProvider)
        {
            _playerView = playerView;
            _cameraProvider = cameraProvider;
            _disposable = new CompositeDisposable();
        }

        public void Initialize()
        {
            _cameraProvider.Camera.Follow = _playerView.CameraRoot;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}