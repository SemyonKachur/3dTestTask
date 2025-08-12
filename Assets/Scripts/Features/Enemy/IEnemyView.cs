using System;
using UnityEngine;
using UnityEngine.AI;

namespace Features.Enemy
{
    public interface IEnemyView : IDisposable
    {
        public NavMeshAgent NavMeshAgent { get; }
        public Animator Animator { get; }
        
        void SetSpeed(float speedCurrentValue);
        
        void SetTarget(Transform target);

        void SetStopDistance(float stopDistanceCurrentValue);
    }
}