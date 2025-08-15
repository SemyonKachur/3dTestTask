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
        private const int Multiplier = 10;
        
        public bool IsSelected => toggle.isOn;
        
        public ReactiveCommand<CharacterStatTypeId> OnStatSelected;
        public IObservable<Unit> OnUpgradeButtonClicked => _upgradeButton.OnClickAsObservable();
        public CharacterStatTypeId StatId { get; private set; }

        private IUpgradableCharacterStat _stat;
        
        [SerializeField] private TMP_Text _statName;
        [SerializeField] private Image _statValue;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private TMP_Text _currentValue;
        
        private RectTransform _rect;
        private CompositeDisposable _disposable;

        public void Construct(IUpgradableCharacterStat stat)
        {
            _disposable?.Dispose();
            _disposable = new CompositeDisposable();
            
            _stat = stat;
            StatId = stat.Id;
            OnStatSelected = new();
            _rect = _statValue.GetComponent<RectTransform>();

            Subscribe();
            SetupView();
        }

        private void SetupView()
        {
            _statName.text = _stat.Name;
            _currentValue.text = $"Current value: {_stat.CurrentValue.Value}";
            _rect.sizeDelta = new Vector2(_stat.MaxValue * Multiplier, _rect.sizeDelta.y);
        }

        private void Subscribe()
        {
            _stat.CurrentLevel.Subscribe(x => SetupView()).AddTo(_disposable);
        }

        public void SetUpgradeInteractable(bool interactable) => _upgradeButton.interactable = interactable;

        protected override void OnToggleValueChanged(bool isOn)
        {
            if (_stat != null && isOn)
            {
                OnStatSelected.Execute(_stat.Id);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _disposable?.Dispose();
        }
    }
}