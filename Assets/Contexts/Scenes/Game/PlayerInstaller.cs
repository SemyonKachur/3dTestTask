using Features.CameraControl;
using Features.Player;
using Features.Player.Model;
using Features.Player.View;
using Infrastructure.Services.InputService;
using UnityEngine;
using Zenject;

namespace Contexts.Scenes.Game
{
    public class PlayerInstaller : MonoInstaller 
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private CameraProvider _cameraProvider;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            
            Container.Bind<IPlayerModel>().To<PlayerModel>().AsSingle();
            
            var playerViewInstance = Container.InstantiatePrefabForComponent<PlayerView>(_playerView, _spawnPoint);
            Container.Bind<IPlayerView>().To<PlayerView>().FromInstance(playerViewInstance).AsSingle();
            
            Container.Bind<ICameraProvider>().FromInstance(_cameraProvider).AsSingle();
            Container.BindInterfacesAndSelfTo<CameraFollowPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraRotationPresenter>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerMovePresenter>().AsSingle();
        }
    }
}