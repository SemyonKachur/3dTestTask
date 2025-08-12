using Features.Levels;
using Features.Player.BaseAttack;
using Infrastructure.Factory;
using Infrastructure.Services.Pools;
using StaticData;
using UnityEngine;
using Zenject;

namespace Contexts.Scenes.Game
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Transform _uiRoot;
        [SerializeField] private LevelStaticData _staticData;
        [SerializeField] private Bullet _bulletPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_staticData).AsSingle();
            Container.BindInstance(_bulletPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<BulletsPool>().AsSingle();
            
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<InfinityEnemiesGameMode>().AsSingle();
        }
    }
}