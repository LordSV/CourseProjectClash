using System;
using UnityEditor;
using UnityEngine;
using static UnitState;

[RequireComponent(typeof(UnitParameters), typeof(Health), typeof(UnitAnimation))]
public class Unit : MonoBehaviour, IHealth, IDestroyed
{
    public event Action Destroyed;
    [field: SerializeField] public Health health { get; private set; }

    [field: SerializeField] public UnitParameters parameters;
    [field: SerializeField] public bool isEnemy { get; private set; } = false;
    [SerializeField] private UnitAnimation _animation;
    [SerializeField] private UnitState _defaultStateSO;
    [SerializeField] private UnitState _chaseStateSO;
    [SerializeField] private UnitState _attackStateSO;
    private UnitState _defaultState;
    private UnitState _chaseState;
    private UnitState _attackState;
    private UnitState _currentState;



    private void Start()
    {
        _animation.Init(this);
        CreateStates();

        _currentState = _defaultState;
        _currentState.Init();

        health.UpdateHealth += CheckDestroy;
    }
    private void Update()
    {
        _currentState.Run();
    }

    private void CreateStates()
    {
        _defaultState = Instantiate(_defaultStateSO);
        _defaultState.Constructor(this);

        _chaseState = Instantiate(_chaseStateSO);
        _chaseState.Constructor(this);

        _attackState = Instantiate(_attackStateSO);
        _attackState.Constructor(this);
    }

    private void CheckDestroy(float currentHP)
    {
        if (currentHP > 0) return;
        health.UpdateHealth -= CheckDestroy;
        Destroy(gameObject);
        Destroyed?.Invoke();
    }

    public void SetState(UnitStateType type)
    {
        _currentState.Finish();
        switch (type)
        {
            case UnitStateType.Default:
                _currentState = _defaultState;
                break;
            case UnitStateType.Chase:
                _currentState = _chaseState;
                break;
            case UnitStateType.Attack:
                _currentState = _attackState;
                break;
            default:
                Debug.LogError("�� �������������� ��������� " + type);
                break;
        }
        _currentState.Init();
        _animation.SetState(type);
    }
#if UNITY_EDITOR
    [Space]
    [SerializeField] private bool _debug = false;

    private void OnDrawGizmos()
    {
        if (_chaseStateSO != null) _chaseStateSO.DebugDrowDistance(this);
    }
#endif
}
