using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class UnitStateNavMeshMove : UnitState
{
    private NavMeshAgent _agent;
    private Vector3 _targetPosition;
    protected bool _targetIsEnemy;
    protected Tower _nearestTower;

    public override void Constructor(Unit unit)
    {
        base.Constructor(unit);

        _targetIsEnemy = _unit.isEnemy == false;

        _agent = _unit.GetComponent<NavMeshAgent>();
        if (_agent == null) Debug.LogError($"На персонаже {unit.name} нет компонента NavMeshAgent");
        _agent.speed = _unit.parameters.speed;
        _agent.radius = _unit.parameters.modelRadius;
        _agent.stoppingDistance = _unit.parameters.startAttackDistance;
    }
    public override void Init()
    {
        Vector3 unitPosition = _unit.transform.position;
        _nearestTower = MapInfo.Instance.GetNearestTower(in unitPosition, _targetIsEnemy);
        _targetPosition = _nearestTower.transform.position;
        _agent.SetDestination(_targetPosition);
    }

    public override void Run()
    {
        if(TryFindTarget(out UnitStateType changeType))
        {
            _unit.SetState(changeType);
        }
    }

    protected abstract bool TryFindTarget(out UnitStateType changeType);

    private bool TryAttackTower()
    {
        float distanceToTarget = _nearestTower.GetDistance(_unit.transform.position);
        if (distanceToTarget <= _unit.parameters.startAttackDistance)
        {
            _unit.SetState(UnitStateType.Attack);
            return true;
        }
        return false;
    }
    private bool TryAttackUnit()
    {
        bool hasEnemy = MapInfo.Instance.TryGetNearestAnyUnit(_unit.transform.position, _targetIsEnemy, out Unit enemy, out float distance);
        if (hasEnemy == false) return false;

        if (_unit.parameters.startChaseDistance >= distance + enemy.parameters.modelRadius)
        {
            _unit.SetState(UnitStateType.Chase);
            return true;
        }

        return false;
    }

    public override void Finish()
    {
        _agent.SetDestination(_unit.transform.position);
    }
}
