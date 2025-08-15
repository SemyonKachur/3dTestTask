using System;
using System.Collections.Generic;
using System.Linq;
using Features.Inventory;
using Features.Player.Stats;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UpgradeWindow
{
    public class UpgradeStatWindowView : MonoBehaviour, IWindowView
    {
        private const string AvailableResourceFormat = "<color=#2BE71D>{0}</color>/{1}";
        private const string UnavailableResourceFormat = "<color=#FF0000>{0}</color>/{1}";
        
        public IObservable<Unit> OnBackClicked => _backButton.OnClickAsObservable();
        public IObservable<Unit> OnApplyClicked => _applyButton.OnClickAsObservable();

        public ReactiveCommand<CharacterStatTypeId> OnUpgradeClicked = new();
        
        
        [SerializeField] private List<UpgradableCharacterStatToggle> _heroStats = new();
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _applyButton;

        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _upgradeTypeText;
        [SerializeField] private TMP_Text _upgradeValueText;
        [SerializeField] private TMP_Text _costText;
        
        private CompositeDisposable _disposables;
        private List<IUpgradableCharacterStat> _upgradableStats = new();
        private IItem _playerExperience;
        
        public void Show()
        { 
            gameObject.SetActive(true);
            var activeToggleType = _heroStats.First(x => x.IsSelected).StatId;
            var stat = _upgradableStats.First(x => x.Id == activeToggleType);
            SetView(stat, _playerExperience);
        }

        public void Hide() => gameObject.SetActive(false);

        public void Construct(List<IUpgradableCharacterStat> upgradableStat, IItem playerExperience)
        {
            _upgradableStats =  upgradableStat;
            _playerExperience = playerExperience;
            
            _disposables?.Dispose();
            _disposables = new();
            
            foreach (var statToggle in _heroStats)
            {
                statToggle.gameObject.SetActive(false);
            }

            for (int i = 0; i < upgradableStat.Count; i++)
            {
                if (_heroStats.Count >= i)
                {
                    var userStat = upgradableStat[i];
                    
                    _heroStats[i].gameObject.SetActive(true);
                    _heroStats[i].Construct(userStat);
                    _heroStats[i].SetUpgradeInteractable(userStat.Cost<= _playerExperience.Count.Value);
                    
                    _heroStats[i].OnUpgradeButtonClicked.
                        Subscribe(x =>
                        {
                            if (_playerExperience.Count.Value >= userStat.Cost)
                            {
                                OnUpgradeClicked.Execute(userStat.Id);
                                userStat.Upgrade();
                                _playerExperience.Count.Value -= userStat.Cost;
                                SetView(userStat, playerExperience);
                            }
                        })
                        .AddTo(_disposables);
                   
                    _heroStats[i].OnStatSelected.Subscribe(x =>
                    {
                        _heroStats[i].SetUpgradeInteractable(userStat.Cost<= _playerExperience.Count.Value);
                        SetView(userStat, playerExperience);
                    }).AddTo(_disposables);
                }
            }
        }

        private void SetView(IUpgradableCharacterStat stat, IItem playerExperience)
        {
            _description.text = stat.Description;
            _upgradeTypeText.text = stat.UpgradeType.ToString();
            _upgradeValueText.text = GetUpgradeTypeString(stat.UpgradeType, stat.AdditionalEffectValue);
            _costText.text = playerExperience.Count.Value >= stat.Cost 
                ? string.Format(AvailableResourceFormat,  playerExperience.Count.Value, stat.Cost)
                : string.Format(UnavailableResourceFormat, playerExperience.Count.Value, stat.Cost);
            _levelText.text = $"Lv.{stat.CurrentLevel.Value}";
        }

        public void SetActiveApplyButton(bool isEnable) => 
            _applyButton.gameObject.SetActive(isEnable);

        private string GetUpgradeTypeString(UpgradeType statUpgradeType, float statAdditionalEffectValue) =>
            statUpgradeType switch
            {
                UpgradeType.AbsoluteValue => $"+{statAdditionalEffectValue}",
                UpgradeType.PercentageFromBaseValue => $"+{statAdditionalEffectValue}%",
                UpgradeType.PercentageFromCurrentValue => $"+{statAdditionalEffectValue}%",
                _ => statAdditionalEffectValue.ToString()
            };
    }
}