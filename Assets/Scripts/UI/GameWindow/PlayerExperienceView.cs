using System;
using Features.Inventory;
using TMPro;
using UniRx;

namespace UI.GameWindow
{
    public class PlayerExperienceView : IDisposable
    {
        private readonly IItem _experience;
        private readonly TMP_Text _experienceText;
        private readonly IDisposable _disposable;

        public PlayerExperienceView(IItem experience, TMP_Text experienceText)
        {
            _experience = experience;
            _experienceText = experienceText;

            _disposable = _experience.Count.Subscribe(UpdateView);
        }

        private void UpdateView(int count) => _experienceText.text = $"Experience: {count}";

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}