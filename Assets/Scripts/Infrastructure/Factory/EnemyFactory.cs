using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Features.Enemy;
using Features.Player.View;
using Infrastructure.Services.ContentProvider;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class EnemyFactory : IEnemyFactory, IDisposable
    {
        private readonly IContentProvider _contentProvider;
        private readonly IPlayerView _playerView;
        private readonly List<IEnemyPresenter> _presenters = new();

        public EnemyFactory(IContentProvider contentProvider, IPlayerView  playerView)
        {
            _contentProvider = contentProvider;
            _playerView = playerView;
        }

        public async UniTask<IEnemyView> CreateEnemy(IEnemyModel enemy, Vector3 position)
        {
            var prefab = await _contentProvider.Load<EnemyView>(enemy.EnemyType.ToString());
            var enemyView = GameObject.Instantiate(prefab, position, Quaternion.identity);
            
            var enemyPresenter = new EnemyPresenter(enemy, enemyView);
            _presenters.Add(enemyPresenter); 
            
            //TODO: добавить компоненты пресследования
            return enemyView;
        }

        public void Dispose()
        {
            foreach (var enemyPresenter in _presenters)
            {
                enemyPresenter.Dispose();
            }
        }
    }
}