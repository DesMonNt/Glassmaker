using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarToEnemy : MonoBehaviour
{
    public Image hpBar;
    private Enemy comp;
    void Start()
    {
        comp = GetComponent<Enemy>();
    }

    private void Update()
    {
        hpBar.fillAmount = comp.currentHealthPoints / comp.CurrentStats.MaxHealth;
    }
}