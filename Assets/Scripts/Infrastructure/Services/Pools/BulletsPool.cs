using System;
using UnityEngine.Pool;
using Zenject;

namespace Infrastructure.Services.Pools
{
    public class BulletsPool : IBulletsPool, IInitializable, IDisposable
    {
        private readonly DiContainer _diContainer;
        private readonly Bullet _prefab;

        private ObjectPool<Bullet> _bulletPool;
        
        public BulletsPool(DiContainer diContainer, Bullet prefab)
        {
            _diContainer = diContainer;
            _prefab = prefab;
        }
        
        public void Initialize()
        {
            _bulletPool = new ObjectPool<Bullet>(
                () => _diContainer.InstantiatePrefabForComponent<Bullet>(_prefab),
                bullet => bullet.gameObject.SetActive(true),
                bullet => bullet.gameObject.SetActive(false),
                bullet => bullet.gameObject.SetActive(false),
                true, 50, 200);
        }

        public Bullet GetBullet() => _bulletPool.Get();

        public void ReturnBullet(Bullet bullet)
        {
            _bulletPool.Release(bullet);
        }

        public void Dispose()
        {
            _bulletPool?.Dispose();
        }
    }
}