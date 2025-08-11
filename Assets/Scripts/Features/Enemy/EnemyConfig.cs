using System.Collections.Generic;
using Features.Player.Stats;
using UnityEngine;

namespace Features.Enemy
{
    [CreateAssetMenu(fileName = nameof(EnemyConfig), menuName = "Enemies/" + nameof(EnemyConfig) )]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyType EnemyType { get; set; }
        [field: SerializeField] public List<CharacterStatConfig> Stats { get; set; }
    }
}