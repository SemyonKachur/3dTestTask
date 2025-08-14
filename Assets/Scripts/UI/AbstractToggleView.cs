using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Toggle))]
    public abstract class AbstractToggleView : MonoBehaviour
    {
        protected Toggle toggle;
        
        protected virtual void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        protected abstract void OnToggleValueChanged(bool isOn);

        protected virtual void OnDestroy() => 
            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }
}