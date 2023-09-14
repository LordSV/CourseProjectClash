using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnitState;

[CreateAssetMenu(fileName = "_UsualTowerAttack", menuName = "UnitState/UsualTowerAttack")]
public class UsualTowerAttack : UnitStateAttack
{
    public override bool TryFindTarget(out float stopAttackDistance)
    {
        Vector3 unitPosition = _unit.transform.position;

        Tower targetTower = MapInfo.Instance.GetNearestTower(unitPosition, _targetIsEnemy);
        if (targetTower.GetDistance(unitPosition) <= _unit.parameters.startAttackDistance)
        {
            _target = targetTower.health;
            stopAttackDistance = _unit.parameters.stopAttackDistance + targetTower.radius;
            return true;
        }
        stopAttackDistance = 0f;
        return false;
    }
}
