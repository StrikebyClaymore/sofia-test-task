using UnityEngine;

namespace SofiaTestTask.Utility
{
    public class CameraFollow : MonoBehaviour, ILateUpdate
    {
        [field: SerializeField] public Camera Camera { get; private set; }
        [SerializeField] private Vector3 _offset;
        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
            Camera.transform.localPosition = _offset;
        }

        public void CustomLateUpdate()
        {
            UpdatePositionAndRotation();
        }

        private void UpdatePositionAndRotation()
        {
            transform.rotation = Quaternion.Euler(_target.eulerAngles.x, _target.eulerAngles.y, 0);
            transform.position = _target.position;
        }
    }
}