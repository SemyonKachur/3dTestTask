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
        private readonly IPlayerModel _playerModel;
        private readonly CompositeDisposable _disposable;

        public PlayerMovePresenter(IInputService inputService, IPlayerView playerView, IPlayerModel playerModel)
        {
            _inputService = inputService;
            _playerView = playerView;
            _playerModel = playerModel;

            _disposable = new();
        }

        public void Initialize()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => MovePlayer(_inputService.Axis.Value))
                .AddTo(_disposable);
        }
        
        private void MovePlayer(Vector2 direction)
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.Value.sqrMagnitude > Constants.Epsilon)
            {
                direction.Normalize();
                
                movementVector.z = direction.y;
                movementVector.x = direction.x;
            }
            
            _playerView.CharacterController.Move(movementVector * _playerModel.MoveSpeed * Time.deltaTime);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}