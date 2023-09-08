using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float max { get; private set; } = 10f;
    [SerializeField] private HealthUI _healthUI;
    private float _current;

    private void Start()
    {
        _current = max;
    }

    public void ApplyDamage(float value)
    {
        _current -= value;
        _healthUI.SetHealth(_current, max);
        if (_current <= 0) 
        {
            _current = 0;
            gameObject.SetActive(false);
        } 

        Debug.Log($"������ {name}: ���� - {_current + value}, ����� {_current}");
    }
}

interface IHealth
{
    Health health { get;}
}
