using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NavMeshRangeChase", menuName = "UnitState/NavMeshRangeChase")]
public class NavMeshRangeChase : UnitStateNavMeshChase
{
    public override void FindTarget(out Unit targetUnit)
    {
        MapInfo.Instance.TryGetNearestAnyUnit(_unit.transform.position, _targetIsEnemy, out targetUnit, out float distance);
    }
}
