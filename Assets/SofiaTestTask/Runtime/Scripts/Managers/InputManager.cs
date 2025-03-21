using UnityEngine;
using UnityEngine.Events;

namespace SofiaTestTask
{
    public class InputManager : IUpdate
    {
        private bool _isTouching;
        private int _activeTouchId = -1;
        public readonly UnityEvent<Vector2> OnRotateInputChanged = new();
        public readonly UnityEvent<Vector2> OnScreenClick = new();

        public void CustomUpdate()
        {
            HandleTouchInput();
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount <= 0)
                return;
            foreach (var touch in Input.touches)
            {
                if ((_activeTouchId != -1 && touch.fingerId != _activeTouchId) ||
                    (UnityEngine.EventSystems.EventSystem.current != null &&
                     UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(touch.fingerId)))
                {
                    continue;
                }

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        OnScreenClick.Invoke(touch.position);
                        _activeTouchId = touch.fingerId;
                        _isTouching = true;
                        break;
                    case TouchPhase.Moved:
                        if (_isTouching)
                            OnRotateInputChanged?.Invoke(touch.deltaPosition);
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        _isTouching = false;
                        _activeTouchId = -1;
                        OnRotateInputChanged?.Invoke(Vector2.zero);
                        break;
                }
                    
                break;
            }
        }
    }
}