using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Features.Inventory;
using Features.Player.Model;
using Features.Player.Stats;
using Infrastructure.Services.Progress;
using Infrastructure.Services.SaveService;
using Infrastructure.Services.StaticDataService;
using Infrastructure.Utils;
using UniRx;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadProgress;
        private readonly IPlayerStaticDataLoader _staticData;
        private readonly IPlayerModel _playerModel;

        public LoadProgressState(GameStateMachine gameStateMachine, IProgressService progressService, 
            ISaveLoadService saveLoadProgress, IPlayerStaticDataLoader staticData, IPlayerModel playerModel)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadProgress = saveLoadProgress;
            _staticData = staticData;
            _playerModel = playerModel;
        }

        public async UniTask Enter()
        {
            await LoadProgressOrInitNew();
      
            _gameStateMachine.Enter<LoadLevelState, string>(Constants.GameLevelName);
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }

        private async UniTask LoadProgressOrInitNew()
        {
            var progress = await _saveLoadProgress.LoadProgress();
            if (progress == null || progress.CharacterStats.Count == 0)
            {
                await SetupNewProgress();
            }
            else
            {
                _progressService.Progress =  progress;
                _playerModel.LoadProgress(_progressService.Progress); 
            }
        }

        private async UniTask SetupNewProgress()
        {
            _progressService.Progress = new PlayerProgress();
            await _staticData.Load();

            _progressService.Progress.CharacterStats = new List<ICharacterStat>();
            foreach (ICharacterStat stat in _staticData.CharacterDefaultStats)
            {
                if (stat is IUpgradableCharacterStat upgradableStat)
                {
                    var upgradableStatModel = new UpgradableCharacterStatModel(
                        upgradableStat.Id, 
                        upgradableStat.Name, 
                        upgradableStat.Description, 
                        upgradableStat.MaxValue, 
                        upgradableStat.CurrentValue.Value, 
                        upgradableStat.DefaultValue, 
                        upgradableStat.UpgradeType, 
                        upgradableStat.MaxLevel, 
                        upgradableStat.AdditionalEffectValue,
                        upgradableStat.Cost);
                    _progressService.Progress.CharacterStats.Add(upgradableStatModel);
                }
                else
                {
                    var statModel = new CharacterStatModel(
                        stat.Id,
                        stat.Name,
                        stat.Description,
                        stat.MaxValue,
                        stat.CurrentValue.Value,
                        stat.DefaultValue);
                    _progressService.Progress.CharacterStats.Add(statModel);
                }
                
            }

            _progressService.Progress.InventoryItems = new List<IItem>();
            foreach (IItem item in _staticData.CharacterDefaultItems)
            {
                var defaultItem = new InventoryItemBase()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Count = new ReactiveProperty<int>(item.Count.Value),
                    Description = item.Description,
                    ImagePath = item.ImagePath,
                    IsStackable = item.IsStackable,
                };
                _progressService.Progress.InventoryItems.Add(defaultItem);
            }
            
            _playerModel.LoadProgress(_progressService.Progress); 
        }
    }
}