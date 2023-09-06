using UnityEngine;

public class UnitParameters : MonoBehaviour
{
    [field: SerializeField] public float speed { get; private set; } = 4f;
    [field: SerializeField] public float startAttackDistance { get; private set; } = 1f;
    [field: SerializeField] public float stopAttackDistance { get; private set; } = 2f;
}
