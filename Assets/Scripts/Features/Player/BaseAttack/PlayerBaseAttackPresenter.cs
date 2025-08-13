using System;
using System.Linq;
using Features.Player.Model;
using Features.Player.Stats;
using Features.Player.View;
using Infrastructure.Services.InputService;
using Infrastructure.Services.Pools;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Player.BaseAttack
{
    public class PlayerBaseAttackPresenter : IPlayerAttackPresenter, IInitializable, IDisposable
    {
        private readonly IPlayerModel _model;
        private readonly IPlayerView _view;
        private readonly IInputService _inputService;
        private readonly IBulletsPool _bulletsPool;
        private ICharacterStat _damageStat;
        
        private CompositeDisposable _disposables;

        private float _delay = 1.0f;
        private float _lastTime = 0;

        public PlayerBaseAttackPresenter(IPlayerModel model, IPlayerView view, IInputService inputService, IBulletsPool bulletsPool)
        {
            _model = model;
            _view = view;
            _inputService = inputService;
            _bulletsPool = bulletsPool;

            _disposables = new();
        }
        
        public void Initialize()
        {
            _damageStat = _model.Stats.FirstOrDefault(x => x.Id == CharacterStatTypeId.Damage);
            Observable.EveryUpdate().Subscribe(x =>
            {
                if (_inputService.IsFire)
                {
                    if (Time.time - _lastTime > _delay)
                    {
                        _lastTime = Time.time;
                     
                        var bullet = _bulletsPool.GetBullet();
                        bullet.transform.position = _view.ShootPoint.position;
                        bullet.transform.rotation = _view.ShootPoint.rotation;
                        bullet.SetDamage(_damageStat.CurrentValue.Value);
                    }
                }
            }).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}