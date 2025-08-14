using System;
using UI.UpgradeWindow;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public sealed class ManMenuView : MonoBehaviour, IWindowView
    {
        public IObservable<Unit> OnContinueClick => _continueButton.OnClickAsObservable();
        public IObservable<Unit> OnQuitClick => _quitButton.OnClickAsObservable();
        public IObservable<Unit> OnUpgradeClick => _upgradeButton.OnClickAsObservable();
        
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _quitButton;
        
        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}