using Features.CameraControl;
using Features.Player.View;
using Infrastructure.Factory;
using Infrastructure.Services.InputService;
using UnityEngine;
using Zenject;

namespace Contexts.Scenes.Game
{
    public class PlayerInstaller : MonoInstaller 
    {
        [SerializeField] private PlayerFactoryData _data;
        [SerializeField] private CinemachineCameraProvider _cameraProvider;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            
            var playerViewInstance = Container.InstantiatePrefabForComponent<PlayerView>(_data.Prefab, _data.SpawnPoint);
            Container.Bind<IPlayerView>().To<PlayerView>().FromInstance(playerViewInstance).AsSingle();
            
            Container.Bind<ICinemachineProvider>().FromInstance(_cameraProvider).AsSingle();
            Container.BindInterfacesAndSelfTo<CameraFollowPresenter>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CharacterControllerInputAdapter>().AsSingle();
        }
    }
}