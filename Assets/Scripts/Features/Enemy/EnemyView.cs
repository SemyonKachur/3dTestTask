using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Features.Enemy
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        [field:SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [field:SerializeField] public Animator Animator { get; private set; }

        private CompositeDisposable _disposables = new();

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
                return;
            }
            
            Observable.Interval(TimeSpan.FromSeconds(.5f))
                .Subscribe(x => NavMeshAgent.SetDestination(target.position))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
            _disposables = null;
            GameObject.Destroy(gameObject);
        }
    }
}