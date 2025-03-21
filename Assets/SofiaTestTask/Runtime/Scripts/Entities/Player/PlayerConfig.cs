using UnityEngine;

namespace SofiaTestTask.Entities
{
    [CreateAssetMenu(menuName = "SofiaTestTask/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 0.1f;
        [field: SerializeField] public float TopClamp { get; private set; } = -90f;
        [field: SerializeField] public float BottomClamp { get; private set; } = 90f;
        [field: SerializeField] public float RaycastInterval { get; private set; } = 0.2f;
        [field: SerializeField] public float RaycastDistance { get; private set; } = 2f;
        [field: SerializeField] public LayerMask RaycastLayer { get; private set; }
        [field: SerializeField] public float ObjectsThrowForce { get; private set; } = 100f;
    }
}