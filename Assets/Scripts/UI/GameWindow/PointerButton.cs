using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.GameWindow
{
    [RequireComponent(typeof(Image))]
    public class PointerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public ReactiveCommand<bool> OnClick = new();
    
        private Image _image;

        private void Awake() => _image = GetComponent<Image>();
    
        public void OnPointerDown(PointerEventData eventData) => OnClick.Execute(true);

        public void OnPointerUp(PointerEventData eventData) => OnClick.Execute(false);
    }
}