using QuickOutline;
using UnityEngine;
using UnityEngine.Events;

namespace SofiaTestTask.Entities
{
    public class Item : MonoBehaviour, ISelectable, IPickable<Item>
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Collider _collider;
        [SerializeField] private Outline _outline;
        public readonly UnityEvent OnPickup = new();

        public void Select()
        {
            _outline.enabled = true;
        }

        public void Deselect()
        {
            _outline.enabled = false;
        }

        public Item Pickup(Transform parent, Vector3 position)
        {
            _rb.isKinematic = true;
            _collider.enabled = false;
            transform.SetParent(parent);
            transform.localPosition = position;
            return this;
        }

        public void Place(Transform parent = null)
        {
            transform.SetParent(parent);
            _rb.isKinematic = true;
            _collider.isTrigger = true;
            _collider.enabled = true;
        }

        public void Throw(Vector3 direction, float force, Transform parent = null)
        {
            transform.SetParent(parent);
            _collider.enabled = true;
            _collider.isTrigger = false;
            _rb.isKinematic = false;
            _rb.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}
