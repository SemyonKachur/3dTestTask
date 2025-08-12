using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Features.Enemy;
using Infrastructure.Services.ContentProvider;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.StaticDataService
{
    public class EnemyStaticDataProvider : IEnemyStaticDataProvider, IInitializable, IDisposable
    {
        private const string StaticDataPath = "StaticData/Enemies/Enemies/";

        public List<EnemyConfig> EnemyConfigs { get; private set; } = new();
        
        private readonly IContentProvider _contentProvider;

        public EnemyStaticDataProvider(IContentProvider contentProvider)
        {
            _contentProvider = contentProvider;
        }
        
        public async UniTask Load()
        {
            var enemyData = await _contentProvider.LoadAll<EnemyConfig>(StaticDataPath);
            if (enemyData != null)
            {
                EnemyConfigs = enemyData.ToList();
            }
            else
            {
                Debug.LogError($"No player data found for {StaticDataPath}");
            }
        }

        public void Initialize()
        {
            Load().Forget();
        }

        public void Dispose()
        {
            EnemyConfigs.Clear();
        }
    }
}