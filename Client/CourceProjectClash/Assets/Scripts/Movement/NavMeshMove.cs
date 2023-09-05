using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NavMeshMove", menuName = "UnitState/NavMeshMove")]
public class NavMeshMove : UnitState
{
    [SerializeField] private float _moveOffset = 5f;
    private NavMeshAgent _agent;
    private Vector3 _targetPosition;

    public override void Init()
    {
        _agent = _unit.GetComponent<NavMeshAgent>();
        _targetPosition = Vector3.forward;
        _agent.SetDestination(_targetPosition);
    }

    public override void Run()
    {
        float distanceToTarget = Vector3.Distance(_unit.transform.position, _targetPosition);
        if(distanceToTarget <= _moveOffset)
        {
            Debug.Log("�������");
            _unit.SetState(UnitStateType.Attack);
        }
    }

    public override void Finish()
    {
     _agent.isStopped = true;
    }
}
