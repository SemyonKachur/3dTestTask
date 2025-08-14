using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.GameWindow
{
    [RequireComponent(typeof(Image))]
    public class LookHandler : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        public Vector2 Look => _look;
        private Vector2 _look;

        public void OnDrag(PointerEventData eventData) => _look = eventData.delta;

        public void OnEndDrag(PointerEventData eventData) => _look = Vector2.zero;
    }
}