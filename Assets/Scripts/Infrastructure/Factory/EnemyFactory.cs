using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Features.Enemy;
using Features.Player.View;
using Infrastructure.Services.ContentProvider;
using Infrastructure.Services.Pools;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class EnemyFactory : IEnemyFactory, IDisposable
    {
        private readonly IContentProvider _contentProvider;
        private readonly IPlayerView _playerView;
        private readonly IBulletsPool _bulletsPool;
        
        private readonly List<IEnemyPresenter> _presenters = new();
        private readonly List<EnemyAttackPresenter> _attackPresenters = new();

        public EnemyFactory(IContentProvider contentProvider, IPlayerView  playerView, IBulletsPool bulletsPool)
        {
            _contentProvider = contentProvider;
            _playerView = playerView;
            _bulletsPool = bulletsPool;
        }

        public async UniTask<IEnemyView> CreateEnemy(IEnemyModel enemy, Vector3 position)
        {
            var prefab = await _contentProvider.Load<EnemyView>(enemy.EnemyType.ToString());
            var enemyView = GameObject.Instantiate(prefab, position, Quaternion.identity);
            
            var enemyPresenter = new EnemyPresenter(enemy, enemyView);
            enemyPresenter.Initialize();
            _presenters.Add(enemyPresenter);
            
            var attackPresenter = new EnemyAttackPresenter(enemy, enemyView, _playerView, _bulletsPool);
            attackPresenter.Initialize();
            _attackPresenters.Add(attackPresenter);
            
            enemyView.SetTarget(_playerView.PlayerRoot);
            
            return enemyView;
        }

        public void Dispose()
        {
            foreach (var enemyPresenter in _presenters)
            {
                enemyPresenter.Dispose();
            }
            _presenters.Clear();

            foreach (var attackPresenter in _attackPresenters)
            {
                attackPresenter.Dispose();
            }
            _attackPresenters.Clear();
        }
    }
}