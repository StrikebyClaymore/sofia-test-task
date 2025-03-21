using SofiaTestTask.UI;
using SofiaTestTask.Utility;
using UnityEngine;

namespace SofiaTestTask.Entities
{
    public class PlayerMovement : IUpdatable
    {
        private readonly PlayerConfig _config;
        private readonly PlayerView _view;
        private readonly CameraFollow _cameraFollow;
        private readonly Rigidbody _rb;
        private readonly Transform _body;
        private Vector3 _moveDirection;
        private Vector2 _rotationVector;
        private Vector2 _rotation;

        public PlayerMovement(PlayerConfig config, PlayerView view, CameraFollow cameraFollow, InputManager inputManager, ControlsController controls)
        {
            _config = config;
            _view = view;
            _cameraFollow = cameraFollow;
            _rb = view.RB;
            _body = view.Body;
            _cameraFollow.SetTarget(_view.CameraFollowPoint);
            inputManager.OnRotateInputChanged.AddListener(RotationChanged);
            controls.OnMoveDirectionChanged.AddListener(MoveDirectionChanged);
        }

        public void CustomUpdate()
        {
            Rotation();
        }

        public void CustomFixedUpdate()
        {
            Move();
        }

        public void CustomLateUpdate()
        {
            _cameraFollow.CustomLateUpdate();
        }
        
        private void Rotation()
        {
            if (_rotationVector == Vector2.zero)
            {
                _rb.angularVelocity = Vector3.zero;
                return;
            }
            _rotation.y += _rotationVector.x * _config.RotationSpeed;
            _rotation.x -= _rotationVector.y * _config.RotationSpeed;
            _rotation.x = Mathf.Clamp(_rotation.x, _config.TopClamp, _config.BottomClamp);
            _body.transform.localEulerAngles = new Vector3(_rotation.x, _rotation.y, 0);
        }

        private void Move()
        {
            if (_moveDirection == Vector3.zero)
            {
                _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
                return;
            }
            var direction = (_body.forward * _moveDirection.z + _body.right * _moveDirection.x).normalized;
            var velocity = direction * _config.MoveSpeed;
            velocity.y = _rb.velocity.y;
            _rb.velocity = velocity;
        }

        private void MoveDirectionChanged(Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                _moveDirection = Vector3.zero;
                return;
            }
            _moveDirection = new Vector3(direction.x, 0, direction.y).normalized;
        }

        private void RotationChanged(Vector2 direction)
        {
            _rotationVector = new Vector2(direction.x, direction.y);
        }
    }
}