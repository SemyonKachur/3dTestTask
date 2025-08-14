using System;
using System.Collections.Generic;
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
        
        [SerializeField] private List<UpgradableCharacterStatToggle> _heroStats = new();
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _applyButton;

        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _upgradeTypeText;
        [SerializeField] private TMP_Text _upgradeValueText;
        [SerializeField] private TMP_Text _costText;
        
        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        public void SetView(IUpgradableCharacterStat stat, IItem playerExperience)
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