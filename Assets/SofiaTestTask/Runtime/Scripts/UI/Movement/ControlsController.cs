using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SofiaTestTask.UI
{
    public class ControlsController : IController
    {
        private readonly ControlsView _view;
        private readonly Canvas _canvas;
        private readonly float _handleRange = 1;
        private readonly float _deadZone = 0;
        private Vector2 _startPosition;
        private bool _enabled;
        private Vector2 _input = Vector2.zero;
        public ControlsView View => _view;
        public readonly UnityEvent<Vector2> OnMoveDirectionChanged = new();

        public ControlsController(ViewsContainer viewsContainer)
        {
            _view = viewsContainer.ControlsView;
            _canvas = viewsContainer.RootCanvas;
            var joystick = _view.Joystick;
            joystick.OnBeginDragEvent.AddListener(Start);
            joystick.OnDragEvent.AddListener(Process);
            joystick.OnEndDragEvent.AddListener(Stop);
        }

        public void Show()
        {
            _view.Show();
        }

        public void Hide()
        {
            _view.Hide();
        }

        private void Start(PointerEventData eventData)
        {
            Vector2 position = eventData.position;
            _view.InnerImage.anchoredPosition = Vector2.zero;
            _startPosition = position;
        }

        private void Process(PointerEventData eventData)
        {
            var direction = (eventData.position - _startPosition).normalized;
            SetInnerPosition(eventData.position);
            OnMoveDirectionChanged?.Invoke(direction);
        }

        private void Stop(PointerEventData eventData)
        {
            _view.InnerImage.anchoredPosition = Vector2.zero;
            OnMoveDirectionChanged?.Invoke(Vector2.zero);
        }

        private void SetInnerPosition(Vector2 eventPosition)
        {
            Vector2 position = RectTransformUtility.WorldToScreenPoint(null, _view.OuterImage.position);
            Vector2 radius = _view.OuterImage.sizeDelta / 2;
            _input = (eventPosition - position) / (radius * _canvas.scaleFactor);
            if (_input.magnitude > _deadZone)
            {
                if (_input.magnitude > 1)
                    _input = _input.normalized;
            }
            else
            {
                _input = Vector2.zero;
            }
            _view.InnerImage.anchoredPosition = _input * radius * _handleRange;
        }
    }
}