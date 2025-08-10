using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Features.Inventory;
using Features.Player.Stats;
using Infrastructure.Services.SaveService;

namespace Infrastructure.Services.StaticDataService
{
    public interface IStaticDataService
    {
        public List<ICharacterStat> CharacterDefaultStats { get; }
        public List<IItem> CharacterDefaultItems { get; }
        
        UniTask Load();
    }
}