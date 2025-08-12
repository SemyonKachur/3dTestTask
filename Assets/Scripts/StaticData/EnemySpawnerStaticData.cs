using System;
using Features.Enemy;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class EnemySpawnerStaticData
    {
        public string Id;
        public EnemyType EnemyType;
        public Vector3 Position;

        public EnemySpawnerStaticData(string id, EnemyType enemyType, Vector3 position)
        {
            Id = id;
            EnemyType = enemyType;
            Position = position;
        }
    }
}