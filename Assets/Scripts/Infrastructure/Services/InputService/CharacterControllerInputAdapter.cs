using System;
using System.Linq;
using Features.Player.Model;
using Features.Player.Stats;
using Features.Player.View;
using Infrastructure.Utils;
using StarterAssets;
using UI.MainMenu;
using UniRx;
using Zenject;

namespace Infrastructure.Services.InputService
{
    public class CharacterControllerInputAdapter : IInitializable,  IDisposable
    {
        private readonly IInputService _input;
        private readonly IPlayerModel _playerModel;
        private readonly StarterAssetsInputs _characterInput;
        private readonly FirstPersonController _characterController;
        private readonly CompositeDisposable _disposable;

        private ICharacterStat _moveStat;
        private float _sprintAdditiveSpeed = 5;

        public CharacterControllerInputAdapter(IInputService input, IPlayerView playerView, IPlayerModel playerModel)
        {
            _input = input;
            _playerModel = playerModel;
            _characterInput = playerView.PlayerRoot.GetComponent<StarterAssetsInputs>();
            _characterController = playerView.PlayerRoot.GetComponent<FirstPersonController>();
            _disposable = new CompositeDisposable();
        }
        
        public void Initialize()
        {
            var moveStat = _playerModel.Stats.FirstOrDefault(x => x.Id == CharacterStatTypeId.Speed);
            if (moveStat != null)
            {
                moveStat.CurrentValue
                    .Subscribe(x =>
                    {
                        _characterController.MoveSpeed = x;
                        _characterController.SprintSpeed = x + _sprintAdditiveSpeed;
                    })
                    .AddTo(_disposable);
            }
            
            Observable.EveryUpdate()
                .Subscribe(x =>
                {
                    _characterInput.move = _input.Move;
                    _characterInput.look = _input.Look * Constants.RotateSpeed;
                    _characterInput.jump = _input.IsJump;
                    _characterInput.sprint = _input.IsRun;
                }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

    }
}