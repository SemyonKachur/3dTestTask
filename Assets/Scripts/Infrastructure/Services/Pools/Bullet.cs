
using Features.Player.BaseAttack;
using UnityEngine;

namespace Infrastructure.Services.Pools
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _speed = 50;
        [SerializeField] private float _activeTime = 5f;
        
        private Rigidbody _rigidbody;
        
        private float _damage;
        private float _startActiveTime;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _startActiveTime = Time.time;
        }

        public void SetDamage(float damage) => _damage = damage;

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent<IDamageReciever>(out var target))
            {
                target.ApplyDamage(_damage);
            }
            
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (isActiveAndEnabled)
            {
                if (Time.time - _startActiveTime > _activeTime)
                {
                    gameObject.SetActive(false);
                    return;
                }
                
                _rigidbody.AddForce(Vector3.forward * _speed);
            }
        }
    }
}