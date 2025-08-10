using Infrastructure.Services.SceneLoader;
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
        }
    }
}