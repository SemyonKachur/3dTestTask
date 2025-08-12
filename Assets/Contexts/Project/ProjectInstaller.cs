using Features.Player.Model;
using Infrastructure.Factory;
using Infrastructure.Services.AutorizationService;
using Infrastructure.Services.ContentProvider;
using Infrastructure.Services.Progress;
using Infrastructure.Services.SaveService;
using Infrastructure.Services.SceneLoader;
using Infrastructure.Services.StaticDataService;
using UnityEngine;
using Zenject;

namespace Contexts.Project
{
    [CreateAssetMenu(fileName = nameof(ProjectInstaller), menuName = "Installers/" + nameof(ProjectInstaller))]
    public class ProjectInstaller : ScriptableObjectInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IContentProvider>().To<ResourcesContentProvider>().AsSingle();
            Container.Bind<IProgressService>().To<ProgressService>().AsSingle();
            Container.Bind<IPlayerStaticDataProvider>().To<PlayerStaticDataProvider>().AsSingle();
            Container.Bind<ISaveLoadService>().To<JsonSaveLoadService>().AsSingle();
            Container.Bind<IAuthService>().To<MockAuthorizationService>().AsSingle();
            Container.Bind<IPlayerModel>().To<PlayerModel>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<EnemyStaticDataProvider>().AsSingle();
        }
    }
}