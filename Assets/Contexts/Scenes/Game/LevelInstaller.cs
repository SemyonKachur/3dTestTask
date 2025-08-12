using Features.Levels;
using Infrastructure.Factory;
using StaticData;
using UnityEngine;
using Zenject;

namespace Contexts.Scenes.Game
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Transform _uiRoot;
        [SerializeField] private LevelStaticData _staticData;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_staticData).AsSingle();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<InfinityEnemiesGameMode>().AsSingle();
        }
    }
}