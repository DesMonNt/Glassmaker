using System;
using System.Collections;
using System.Collections.Generic;
using FightingScene.Units;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    public Image shieldBar;
    private Unit _comp;
    void Start() => _comp = GetComponent<Unit>();

    private void Update() => 
        shieldBar.fillAmount = (float)Math.Round((double)_comp.currentShield / _comp.CurrentStats.MaxHealth, 2);
}
