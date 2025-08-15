using System;
using UnityEngine;
using Zenject;

namespace Infrastructure.Utils
{
    public class SceneDisposer : MonoBehaviour
    {
        [Inject] private DiContainer _container;

        private void OnDisable()
        {
            DisposeAll();
        }
        
        private void DisposeAll()
        {
            foreach (var obj in _container?.ResolveAll<IDisposable>())
            {
                obj.Dispose();
            }
        }
    }
}