using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitStateAttack : UnitState
{
    [SerializeField] private float _damage = 1.5f;
    [SerializeField] private float _delay = 1f;
    private float _time = 0f;
    private float _stopAttackDistance = 0;
    protected bool _targetIsEnemy;
    protected Health _target;
    public override void Constructor(Unit unit)
    {
        base.Constructor(unit);

        _targetIsEnemy = _unit.isEnemy == false;
    }
    public override void Init()
    {
        if (TryFindTarget(out _stopAttackDistance) == false)
        {
            _unit.SetState(UnitStateType.Default);
            return;
        }
        _time = 0f;
        _unit.transform.LookAt(_target.transform.position);
    }

    public abstract bool TryFindTarget(out float stopAttackDistance);

    public override void Run()
    {
        _time += Time.deltaTime;
        if (_time < _delay) return;
        _time -= _delay;


        if (_target == false)
        {
            _unit.SetState(UnitStateType.Default);
            return;
        }

        float distanceToTarget = Vector3.Distance(_target.transform.position, _unit.transform.position);
        if (distanceToTarget > _stopAttackDistance) _unit.SetState(UnitStateType.Chase);

        _target.ApplyDamage(_damage);
    }
    public override void Finish()
    {

    }
}
