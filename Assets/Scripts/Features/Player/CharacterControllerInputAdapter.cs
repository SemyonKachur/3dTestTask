using System;
using System.Linq;
using Features.Player.Model;
using Features.Player.Stats;
using Features.Player.View;
using Infrastructure.Services.InputService;
using Infrastructure.Utils;
using StarterAssets;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Player
{
    public class CharacterControllerInputAdapter : IInitializable,  IDisposable
    {
        private readonly IInputService _input;
        private readonly IPlayerView _playerView;
        private readonly IPlayerModel _playerModel;
        private readonly StarterAssetsInputs _characterInput;
        private readonly CompositeDisposable _disposable;

        private ICharacterStat _moveStat;
        private float multiplier = 1;

        public CharacterControllerInputAdapter(IInputService input, IPlayerView playerView, IPlayerModel playerModel)
        {
            _input = input;
            _playerView = playerView;
            _playerModel = playerModel;
            _characterInput = playerView.PlayerRoot.GetComponent<StarterAssetsInputs>();
            _disposable = new CompositeDisposable();
        }
        
        public void Initialize()
        {
            var moveStat = _playerModel.Stats.FirstOrDefault(x => x.Id == CharacterStatTypeId.Speed);
            if (moveStat != null)
            {
                moveStat.CurrentValue
                    .Subscribe(x => multiplier = x)
                    .AddTo(_disposable);
            }
            
            Observable.EveryUpdate()
                .Subscribe(x =>
                {
                    _characterInput.move = _input.Move * multiplier * Time.deltaTime;
                    _characterInput.look = _input.Look * Constants.RotateSpeed;
                }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}