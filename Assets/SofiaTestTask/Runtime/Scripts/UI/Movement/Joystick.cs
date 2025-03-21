using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SofiaTestTask.UI
{
    [RequireComponent(typeof(Image))]
    public class Joystick : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public readonly UnityEvent<PointerEventData> OnBeginDragEvent = new();
        public readonly UnityEvent<PointerEventData> OnEndDragEvent = new();
        public readonly UnityEvent<PointerEventData> OnDragEvent = new();

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnBeginDragEvent?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEndDragEvent?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragEvent?.Invoke(eventData);
        }
    }
}