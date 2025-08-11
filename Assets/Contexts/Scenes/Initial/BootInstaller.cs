using Infrastructure.Services.Boot;
using UnityEngine;
using Zenject;

namespace Contexts.Scenes.Initial
{
    public class BootInstaller : MonoInstaller
    {
        [SerializeField] private Boot _boot;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_boot).AsSingle().NonLazy();
        }
    }
}
