using System;
using Features.Player.Stats;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameWindow
{
    public class PlayerHealthView : IDisposable
    {
        private const int Multiplier = 10;
        
        private readonly IUpgradableCharacterStat _playerHealth;
        private readonly Slider _healthBar;
        private readonly TMP_Text _healthText;
        private readonly RectTransform _healthBarRectTransform;
        
        private CompositeDisposable _disposables = new CompositeDisposable();

        public PlayerHealthView(IUpgradableCharacterStat playerHealth, Slider healthBar, TMP_Text healthText)
        {
            _playerHealth = playerHealth;
            _healthBar = healthBar;
            _healthText = healthText;
            
            _healthBarRectTransform = _healthBar.GetComponent<RectTransform>();

            _playerHealth.CurrentLevel
                .Subscribe(InitSliderValues)
                .AddTo(_disposables);
            _playerHealth.CurrentValue
                .Subscribe(UpdateHealthValue)
                .AddTo(_disposables);
        }

        private void UpdateHealthValue(float health)
        {
            _healthBar.value = health;
            _healthText.text = $"{health}/{_playerHealth.MaxValue}";
        } 

        private void InitSliderValues(int level)
        {
            _healthBar.maxValue = _playerHealth.MaxValue;
            _healthBar.value = _playerHealth.CurrentValue.Value;
            
            _healthBarRectTransform.sizeDelta = new Vector2(_healthBar.maxValue * Multiplier, _healthBarRectTransform.sizeDelta.y);
        }


        public void Dispose() => _disposables.Dispose();
    }
}