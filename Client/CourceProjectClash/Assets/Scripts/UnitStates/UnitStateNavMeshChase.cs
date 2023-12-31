using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public abstract class UnitStateNavMeshChase : UnitState
{
    private NavMeshAgent _agent;
    protected bool _targetIsEnemy;
    protected Unit _targetUnit;
    protected float _startAttackDistance = 0;
    public override void Constructor(Unit unit)
    {
        base.Constructor(unit);

        _targetIsEnemy = _unit.isEnemy == false;

        _agent = _unit.GetComponent<NavMeshAgent>();
        if (_agent == null) Debug.LogError($"�� ��������� {unit.name} ��� ���������� NavMeshAgent");
    }
    public override void Init()
    {
       FindTarget(out _targetUnit);
       if (_targetUnit == null) _unit.SetState(UnitStateType.Default);
    }

    public abstract void FindTarget(out Unit targetUnit);

    public override void Run()
    {
        if (_targetUnit == null)
        {
            _unit.SetState(UnitStateType.Default);
            return;
        }

        float distanceToTarget = Vector3.Distance(_unit.transform.position, _targetUnit.transform.position);
        if (distanceToTarget > _unit.parameters.stopChaseDistance) _unit.SetState(UnitStateType.Default);
        else if (distanceToTarget <= _unit.parameters.startAttackDistance + _targetUnit.parameters.modelRadius) _unit.SetState(UnitStateType.Attack);
        else _agent.SetDestination(_targetUnit.transform.position);
    }
    public override void Finish()
    {
        _agent.SetDestination(_unit.transform.position);
    }
#if UNITY_EDITOR
    public override void DebugDrowDistance(Unit unit)
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.parameters.startChaseDistance);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.parameters.stopChaseDistance);
    }
#endif
}
