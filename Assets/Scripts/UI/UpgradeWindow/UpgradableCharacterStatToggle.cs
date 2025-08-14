using System;
using Features.Player.Stats;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UpgradeWindow
{
    public class UpgradableCharacterStatToggle : AbstractToggleView
    {
        public ReactiveCommand<CharacterStatTypeId> OnStatSelected;
        public IObservable<Unit> OnUpgradeButtonClicked => _upgradeButton.OnClickAsObservable();
        
        private IUpgradableCharacterStat _stat;
        
        [SerializeField] private TMP_Text _statName;
        [SerializeField] private Image _statValue;
        [SerializeField] private Button _upgradeButton;

        public void Construct(IUpgradableCharacterStat stat)
        {
            _stat = stat;
            OnStatSelected = new();
        }

        protected override void OnToggleValueChanged(bool isOn)
        {
            if (_stat != null && isOn)
            {
                OnStatSelected.Execute(_stat.Id);
            }
        }
    }
}