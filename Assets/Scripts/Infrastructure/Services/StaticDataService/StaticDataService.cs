using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Features.Inventory;
using Features.Player.Stats;
using Infrastructure.Services.ContentProvider;
using Infrastructure.Services.SaveService;
using StaticData;

namespace Infrastructure.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private const string PlayerStaticDataPath = "StaticData/Player";
        
        private readonly IContentProvider _contentProvider;

        public StaticDataService(IContentProvider contentProvider)
        {
            _contentProvider = contentProvider;
        }

        public List<ICharacterStat> CharacterDefaultStats { get; private set; } = new();
        public List<IItem> CharacterDefaultItems { get; private set; } = new();
        
        public async UniTask Load()
        {
            var playerData = await _contentProvider.Load<PlayerStaticData>(PlayerStaticDataPath);
            if (playerData != null)
            {
                CharacterDefaultStats = playerData.PlayerConfigs
                    .OfType<ICharacterStat>()
                    .ToList();

                CharacterDefaultItems = playerData.ItemConfigs
                    .OfType<IItem>()
                    .ToList();
            }
        }
    }
}