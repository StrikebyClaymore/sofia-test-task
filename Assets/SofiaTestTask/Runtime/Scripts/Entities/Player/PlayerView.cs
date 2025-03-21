using UnityEngine;

namespace SofiaTestTask.Entities
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody RB { get; private set; }
        [field: SerializeField] public Transform Body { get; private set; }
        [field: SerializeField] public Transform Hands { get; private set; }
        [field: SerializeField] public Transform CameraFollowPoint { get; private set; }
    }
}