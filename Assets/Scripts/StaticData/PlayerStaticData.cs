using System.Collections.Generic;
using Features.Inventory;
using Features.Player.Stats;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = nameof(PlayerStaticData), menuName = "StaticData/" +  nameof(PlayerStaticData))]
    public class PlayerStaticData : ScriptableObject
    {
        public List<CharacterStatConfig> PlayerConfigs;
        public List<ItemConfig> ItemConfigs;
    }
}