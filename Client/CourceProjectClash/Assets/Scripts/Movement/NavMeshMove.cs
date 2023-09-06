using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NavMeshMove", menuName = "UnitState/NavMeshMove")]
public class NavMeshMove : UnitState
{
    private NavMeshAgent _agent;
    private Vector3 _targetPosition;
    private bool _targetIsEnemy;

    public override void Constructor(Unit unit)
    {
        base.Constructor(unit);

        _targetIsEnemy = _unit.isEnemy == false;

        _agent = _unit.GetComponent<NavMeshAgent>();
        if (_agent == null) Debug.LogError($"На персонаже {unit.name} нет компонента NavMeshAgent");
        _agent.speed = _unit.parameters.speed;
        _agent.stoppingDistance = _unit.parameters.startAttackDistance;

    }
    public override void Init()
    {
        Vector3 unitPosition = _unit.transform.position;
        _targetPosition = MapInfo.Instance.GetNearestTowerPosition(in unitPosition, _targetIsEnemy);
        _agent.SetDestination(_targetPosition);
    }

    public override void Run()
    {
        float distanceToTarget = Vector3.Distance(_unit.transform.position, _targetPosition);
        if(distanceToTarget <=_unit.parameters.startAttackDistance)
        {
            Debug.Log("Добежал");
            _unit.SetState(UnitStateType.Attack);
        }
    }

    public override void Finish()
    {
     _agent.isStopped = true;
    }
}
