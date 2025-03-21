using System.Collections;
using UnityEngine;

namespace SofiaTestTask.Entities
{
    public class ShopDoors : MonoBehaviour
    {
        [SerializeField] private Transform _doorLeft;
        [SerializeField] private Transform _doorRight;
        [SerializeField] private Vector3 _doorLeftOpenPosition;
        [SerializeField] private Vector3 _doorLeftClosePosition;
        [SerializeField] private Vector3 _doorRightOpenPosition;
        [SerializeField] private Vector3 _doorRightClosePosition;
        [SerializeField] private float _doorsOpenDuration = 3f;

        private IEnumerator MoveDoorsCycle(Vector3 leftPosition, Vector3 rightPosition)
        {
            var progress = 0f;
            var positionLeft = _doorLeft.transform.localPosition;
            var positionRight = _doorRight.transform.localPosition;
            while (progress < 1f)
            {
                progress += Time.unscaledDeltaTime * (1.0f / _doorsOpenDuration);
                _doorLeft.transform.localPosition = Vector3.Lerp(positionLeft, leftPosition, progress);
                _doorRight.transform.localPosition = Vector3.Lerp(positionRight, rightPosition, progress);
                yield return null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerView>(out _))
            {
                StartCoroutine(MoveDoorsCycle(_doorLeftOpenPosition, _doorRightOpenPosition));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            StartCoroutine(MoveDoorsCycle(_doorLeftClosePosition, _doorRightClosePosition));
        }
    }
}