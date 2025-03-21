using UnityEngine;

namespace SofiaTestTask.Entities
{
    public interface IPickable<out T>
    {
        T Pickup(Transform parent, Vector3 position);
        void Place(Transform parent = null);
        void Throw(Vector3 direction, float force, Transform parent = null);
    }
}