using SofiaTestTask.UI;
using UnityEngine;

namespace SofiaTestTask.Entities
{
    public class PlayerInteractions : IUpdate
    {
        private readonly PlayerConfig _config;
        private readonly Transform _hands;
        private readonly Camera _camera;
        private readonly ControlsController _controls;
        private float _raycastTimer = 0f;
        private Item _itemInHands;
        private ISelectable _selectedObject;
        
        
        public PlayerInteractions(PlayerConfig config, PlayerView view, InputManager inputManager, Camera camera, ControlsController controls)
        {
            _config = config;
            _camera = camera;
            _hands = view.Hands;
            _controls = controls;
            _controls.View.DropButton.onClick.AddListener(DropObject);
            inputManager.OnScreenClick.AddListener(ClickAction);
        }

        public void CustomUpdate()
        {
            RaycastDetection();
        }
        
        private void ClickAction(Vector2 position)
        {
            Ray ray =_camera.ScreenPointToRay(position);
            if (Physics.Raycast(ray, out var hit) && hit.transform.TryGetComponent<ISelectable>(out var selectable) && selectable == _selectedObject)
            {
                switch (_selectedObject)
                {
                    case Car car:
                        _itemInHands = car.TryPutItem(_itemInHands);
                        break;
                    case Item item:
                        _itemInHands = item.Pickup(_hands, _hands.localPosition);
                        break;
                }
                _controls.View.DropButton.gameObject.SetActive(_itemInHands != null);
            }
        }
        
        private void RaycastDetection()
        {
            _raycastTimer += Time.deltaTime;
            if (_raycastTimer >= _config.RaycastInterval)
            {
                _raycastTimer = 0f;
                Vector3 rayOrigin = _camera.transform.position;
                Vector3 rayDirection = _camera.transform.forward;
                if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, _config.RaycastDistance, _config.RaycastLayer))
                {
                    SelectObject(hit.collider.gameObject);
                }
                else
                {
                    DeselectObject();
                }
            }
        }

        private void SelectObject(GameObject obj)
        {
            if (obj.TryGetComponent<ISelectable>(out var selectable))
            {
                DeselectObject();
                _selectedObject = selectable;
                _selectedObject.Select();
            }
        }

        private void DeselectObject()
        {
            if (_selectedObject != null)
                _selectedObject.Deselect();
        }
        
        private void DropObject()
        {
            _itemInHands.Throw(_camera.transform.forward, _config.ObjectsThrowForce);
            _itemInHands = null;
            DeselectObject();
            _selectedObject = null;
            _controls.View.DropButton.gameObject.SetActive(false);
        }
    }
}