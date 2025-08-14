using Features.Levels;
using Features.Saves;
using Features.Score;
using Infrastructure.Factory;
using Infrastructure.Services.Lifecycle;
using Infrastructure.Services.Pools;
using StaticData;
using UI;
using UI.GameWindow;
using UI.MainMenu;
using UI.UpgradeWindow;
using UnityEngine;
using Zenject;

namespace Contexts.Scenes.Game
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Transform _uiRoot;
        [SerializeField] private LevelStaticData _staticData;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private ApplicationLifecycleService _applicationLifecycleService;
        
        //TODO: Move UI to UI factory and make UI controller. Not enough time.
        [SerializeField] private GameWindowView _gameWindowView;
        [SerializeField] private ManMenuView _manMenuView;
        [SerializeField] private UpgradeStatWindowView _upgradeStatWindowView;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_staticData).AsSingle();
            Container.BindInstance(_bulletPrefab).AsSingle();
            Container.BindInstance(_applicationLifecycleService).AsSingle();
            
            Container.BindInterfacesAndSelfTo<BulletsPool>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<InfinityEnemiesGameMode>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerExperienceProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<IntervalSaveController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LifecycleSaveController>().AsSingle();
            
            Container.BindInstance(_gameWindowView).AsSingle();
            Container.BindInstance(_manMenuView).AsSingle();
            Container.BindInstance(_upgradeStatWindowView).AsSingle();

            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameWindowPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeWindowPresenter>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<UINavigator>().AsSingle();
        }
    }
}