using System;
using System.Collections.Generic;
using Features.Inventory;
using Features.Player.Stats;

namespace Infrastructure.Services.SaveService
{
    [Serializable]
    public class PlayerProgress
    {
        public List<ICharacterStat> CharacterStats;
        public List<IItem> InventoryItems;
    }
}