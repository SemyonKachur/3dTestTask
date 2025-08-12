using System.Collections.Generic;
using Features.Inventory;
using Features.Player.Stats;

namespace Infrastructure.Services.StaticDataService
{
    public interface IPlayerStaticDataProvider :  IStaticDataLoader
    {
        public List<ICharacterStat> CharacterDefaultStats { get; }
        public List<IItem> CharacterDefaultItems { get; }
    }
}