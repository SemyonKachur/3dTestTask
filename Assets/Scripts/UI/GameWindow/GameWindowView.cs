using System;
using Features.Inventory;
using Features.Player.Stats;
using TMPro;
using UI.UpgradeWindow;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameWindow
{
    public class GameWindowView : MonoBehaviour, IWindowView
    {
        public IObservable<Unit> OnMenuButtonClicked => _menuButton.OnClickAsObservable();

        public ReactiveCommand<bool> OnAttackClicked => _attackButton.OnClick;
        public ReactiveCommand<bool> OnRunClicked => _runButton.OnClick;
        public ReactiveCommand<bool> OnJumpClicked => _jumpButton.OnClick;
        
        public Vector2 Move => _joystick.Direction;
        public Vector2 Look => _lookHandler.Look;

        [SerializeField] private Slider _healthBar;
        [SerializeField] private TMP_Text _healthValue;
        [SerializeField] private TMP_Text _experienceText;
        [SerializeField] private Button _menuButton;
        
        [SerializeField] private FixedJoystick _joystick;
        [SerializeField] private PointerButton _attackButton;
        [SerializeField] private PointerButton _runButton;
        [SerializeField] private PointerButton _jumpButton;
        [SerializeField] private LookHandler _lookHandler;
        
        private PlayerExperienceView _playerExperienceView;
        private PlayerHealthView _playerHealthView;
        
        private IUpgradableCharacterStat _playerHealth;
        private IItem _experience;

        public void Construct(IUpgradableCharacterStat playerHealth, IItem experience)
        {
            _playerHealth = playerHealth;
            _experience = experience;
            
            _playerHealthView = new PlayerHealthView(_playerHealth, _healthBar, _healthValue);
            _playerExperienceView = new PlayerExperienceView(_experience, _experienceText); 
        }

        public void ShowMobileInput(bool isShow)
        {
            _joystick.gameObject.SetActive(isShow);
            _attackButton.gameObject.SetActive(isShow);
            _runButton.gameObject.SetActive(isShow);
            _jumpButton.gameObject.SetActive(isShow);
            _lookHandler.gameObject.SetActive(isShow);
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        private void OnDestroy()
        {
            _playerHealthView?.Dispose();
            _playerExperienceView?.Dispose();
        }
    }
}