using System.Collections.Generic;
using System.Linq;
using Features.Inventory;
using Features.Player.Model;
using UniRx;
using UnityEngine;

namespace Features.Score
{
    public class PlayerExperienceProvider : IPlayerExperienceProvider
    {
        private readonly List<IExperienceScoreHolder> _experienceScoreHolders;
        private readonly IPlayerModel _playerModel;

        private CompositeDisposable _disposables;
        private IItem _playerExperience;
        private int _startValue;

        public PlayerExperienceProvider(List<IExperienceScoreHolder> experienceScoreHolders, IPlayerModel playerModel)
        {
            _experienceScoreHolders = experienceScoreHolders;
            _playerModel = playerModel;
        }
        
        public void Initialize()
        {
            _disposables =  new();
            _playerExperience = _playerModel.Items.First(x => x.ItemType == ItemType.Experience);
            _startValue =  _playerExperience.Count.Value;
            Debug.LogError($"Start player experience: {_startValue}");
            
            foreach (var experienceScoreHolder in _experienceScoreHolders)
            {
                experienceScoreHolder.ExperienceCounter
                    .Subscribe(AddToUserScore)
                    .AddTo(_disposables);
            }
        }
        
        public void AddToUserScore(int value)
        {
            var result = _experienceScoreHolders.Sum(x => x.ExperienceCounter.Value);
            _playerExperience.Count.Value = _startValue + result;
            Debug.LogError($"Current player experience: {_playerExperience.Count.Value}");
        }
        
        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}