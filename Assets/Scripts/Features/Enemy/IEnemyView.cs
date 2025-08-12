using System;
using Features.Player.BaseAttack;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Features.Enemy
{
    public interface IEnemyView : IDisposable, IDamageReciever
    {
        public ReactiveCommand<float> OnDamageRecieved { get; }
        public NavMeshAgent NavMeshAgent { get; }
        public Animator Animator { get; }
        
        void SetSpeed(float speedCurrentValue);
        
        void SetTarget(Transform target);

        void SetStopDistance(float stopDistanceCurrentValue);
    }
}