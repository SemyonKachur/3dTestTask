using System;
using System.Collections.Generic;
using System.Linq;
using Features.Inventory;
using Features.Player.Model;
using Features.Player.Stats;
using UI.MainMenu;
using UniRx;
using Zenject;

namespace UI.UpgradeWindow
{
    public class UpgradeWindowPresenter : IInitializable, IDisposable, IWindowPresenter
    {
        public ReactiveCommand<WindowType> OnShowWindow { get; } = new();
        public WindowType WindowType => WindowType.UpgradeWindow;
        
        private readonly UpgradeStatWindowView _view;
        private readonly IPlayerModel _playerModel;

        private CompositeDisposable _disposables;
        private List<IUpgradableCharacterStat> _playerUpgradableStats = new();
        private IItem _playerExperience;

        public UpgradeWindowPresenter(UpgradeStatWindowView view, IPlayerModel playerModel)
        {
            _view = view;
            _playerModel = playerModel;
        }

        public void Initialize()
        {
            SetupView();
        }

        private void ApplyModifiedStats(List<IUpgradableCharacterStat> statClones, IItem experienceClone)
        {
            foreach (var statClone in statClones)
            {
                var stat = _playerUpgradableStats.First(x => x.Id == statClone.Id);
                stat.CurrentLevel.Value =  statClone.CurrentLevel.Value;
                stat.MaxValue = statClone.MaxValue;
                stat.CurrentValue.Value =  statClone.CurrentValue.Value;
            }
            
            _playerExperience.Count.Value = experienceClone.Count.Value;
        }

        public void Show()
        {
            SetupView();
            _view.Show();
            _view.SetActiveApplyButton(false);
        }

        public void Hide() => _view.Hide();

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private void SetupView()
        {
            _disposables?.Dispose();
            _disposables = new();
            _playerUpgradableStats = _playerModel.Stats
                .OfType<IUpgradableCharacterStat>()
                .ToList();
            _playerExperience = _playerModel.Items.First(x => x.ItemType == ItemType.Experience);
            
            var statClones = _playerUpgradableStats.Select(x => x.Clone()).ToList();
            var experienceClone = _playerExperience.Clone();
            
            _view.Construct(statClones, experienceClone);
            
            _view.OnBackClicked
                .Subscribe(x => OnShowWindow.Execute(WindowType.MainMenu))
                .AddTo(_disposables);
            _view.OnApplyClicked
                .Subscribe(x =>
                {
                    ApplyModifiedStats(statClones, experienceClone);
                    _view.SetActiveApplyButton(false);
                })
                .AddTo(_disposables);
            _view.OnUpgradeClicked
                .Subscribe(x => _view.SetActiveApplyButton(true))
                .AddTo(_disposables);
        }
    }
}