using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public sealed class SwitchGameObjectsOnToggleChange : AbstractToggleView
    {
        [SerializeField] private List<GameObject> gameObjects = new();

        private void OnEnable() => OnToggleValueChanged(toggle.isOn);

        protected override void OnToggleValueChanged(bool isOn)
        {
            gameObjects.ForEach(x => x.SetActive(isOn));
        }
    }
}