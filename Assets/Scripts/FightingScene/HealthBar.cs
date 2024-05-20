using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpBar;
    private Unit comp;
    void Start() => comp = GetComponent<Unit>();

    private void Update() => 
        hpBar.fillAmount = (float)Math.Round((double)comp.currentHealthPoints / comp.CurrentStats.MaxHealth, 2);
}
