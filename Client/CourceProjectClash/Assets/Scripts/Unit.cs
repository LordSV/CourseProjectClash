using UnityEditor;
using UnityEngine;
using static UnitState;

[RequireComponent(typeof(UnitParameters))]
public class Unit : MonoBehaviour
{
    [field: SerializeField] public UnitParameters parameters;
    [field: SerializeField] public bool isEnemy { get; private set; } = false;
    [SerializeField] private UnitState _defaultStateSO;
    [SerializeField] private UnitState _chaseStateSO;
    [SerializeField] private UnitState _attackStateSO;
    private UnitState _defaultState;
    private UnitState _chaseState;
    private UnitState _attackState;
    private UnitState _currentState;

    private void Start()
    {
        _defaultState = Instantiate(_defaultStateSO);
        _defaultState.Constructor(this);
        _chaseState = Instantiate(_chaseStateSO);
        _chaseState.Constructor(this);
        _attackState = Instantiate(_attackStateSO);
        _attackState.Constructor(this);

        _currentState = _defaultState;
        _currentState.Init();
    }
    private void Update()
    {
        _currentState.Run();
    }

    public void SetState(UnitStateType type)
    {
       _currentState.Finish();
       switch(type)
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
