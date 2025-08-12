using System;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Features.Enemy
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        public ReactiveCommand<float> OnDamageRecieved { get; } = new();
        
        [field:SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [field:SerializeField] public Animator Animator { get; private set; }

        private CompositeDisposable _disposables = new();
        private CancellationTokenSource _cts = new();

        public void SetSpeed(float speedCurrentValue) => 
            NavMeshAgent.speed = speedCurrentValue;

        public void SetStopDistance(float stopDistanceCurrentValue) => 
            NavMeshAgent.stoppingDistance = stopDistanceCurrentValue;

        public void SetTarget(Transform target)
        {
            if (target == null)
            {
                NavMeshAgent.ResetPath();
                _disposables?.Dispose();
                
                _cts.Cancel();
                _cts = null;
                
                return;
            }
            
            _cts = new CancellationTokenSource();
            
            Observable.Interval(TimeSpan.FromSeconds(.5f))
                .TakeUntilDestroy(gameObject)
                .Subscribe(x =>
                {
                    if (_cts is { IsCancellationRequested: false })
                    {
                        NavMeshAgent.SetDestination(target.position);
                    }
                })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _cts.Cancel();
            _cts = null;
            
            Destroy(gameObject);
        }

        public void ApplyDamage(float damage)
        {
            OnDamageRecieved.Execute(damage);
        }
    }
}