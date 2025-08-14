using Features.Player.Stats;
using UniRx;

namespace UI.UpgradeWindow
{
    public class UpgradableStatToggle : AbstractToggleView
    {
        public ReactiveCommand<CharacterStatTypeId> OnStatSelected;
        
        private IUpgradableCharacterStat _stat;

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