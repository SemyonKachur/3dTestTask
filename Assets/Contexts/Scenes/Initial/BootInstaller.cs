using Infrastructure.Services.AutorizationService;
using Infrastructure.Services.Boot;
using Zenject;

namespace Contexts.Scenes.Initial
{
    public class BootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MockAuthorizationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<Boot>().AsSingle();
        }
    }
}
