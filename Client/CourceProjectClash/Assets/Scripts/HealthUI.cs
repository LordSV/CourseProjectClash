using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthImage;

    private void OnEnable()
    {
        _healthImage.fillAmount = 1f;
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        _healthImage.fillAmount = currentHealth / maxHealth;
    }
}
