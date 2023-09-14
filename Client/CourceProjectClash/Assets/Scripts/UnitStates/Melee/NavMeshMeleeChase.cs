using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_NavMeshMeleeChase", menuName = "UnitState/NavMeshMeleeChase")]
public class NavMeshMeleeChase : UnitStateNavMeshChase
{
    public override void FindTarget(out Unit targetUnit)
    {
        MapInfo.Instance.TryGetNearestWalkingUnit(_unit.transform.position, _targetIsEnemy, out targetUnit, out float distance);
    }
}
