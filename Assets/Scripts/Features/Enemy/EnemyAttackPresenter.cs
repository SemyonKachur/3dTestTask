using System;
using System.Linq;
using Features.Player.Stats;
using Features.Player.View;
using Infrastructure.Services.Pools;
using UniRx;
using UnityEngine;

namespace Features.Enemy
{
    public class EnemyAttackPresenter : IDisposable
    {
        private readonly IEnemyModel _model;
        private readonly IEnemyView _view;
        private readonly IPlayerView _playerView;
        private readonly IBulletsPool _bulletsPool;
        
        private ICharacterStat _damageStat;
        private ICharacterStat _attackRange;
        private CompositeDisposable _disposables;

        private float _delay = 1.0f;
        private float _lastTime = 0;
        private float _trashold = 1;

        public EnemyAttackPresenter(IEnemyModel model, IEnemyView view, IPlayerView playerView, IBulletsPool bulletsPool)
        {
            _model = model;
            _view = view;
            _playerView = playerView;
            _bulletsPool = bulletsPool;

            _disposables = new();
        }
        public void Initialize()
        {
            _damageStat = _model.Stats.FirstOrDefault(x => x.Id == CharacterStatTypeId.Damage);
            _attackRange = _model.Stats.FirstOrDefault(x => x.Id == CharacterStatTypeId.AttackRange);
            
            Observable.EveryUpdate().Subscribe(x =>
            {
                if (_model.IsDeath.Value)
                {
                    _disposables.Dispose();
                    return;
                }

                var distance = (_playerView.PlayerRoot.position - _view.NavMeshAgent.gameObject.transform.position)
                    .magnitude;
                if (Time.time - _lastTime > _delay && distance - _trashold <= _attackRange.CurrentValue.Value)
                {
                    _lastTime = Time.time;

                    var bullet = _bulletsPool.GetBullet();
                    bullet.transform.position = _view.ShootPoint.position;
                    bullet.transform.rotation = _view.ShootPoint.rotation;
                    bullet.SetDamage(_damageStat.CurrentValue.Value);
                }
            }).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}