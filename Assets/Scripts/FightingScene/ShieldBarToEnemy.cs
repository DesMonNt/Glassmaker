using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FightingScene.Units;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBarToEnemy : MonoBehaviour
{
    public Image shieldBar;
    private Unit _comp;
    private void Start()
    {
        var assembly = Assembly.GetAssembly(typeof(Unit));
        var unitTypes = assembly.GetTypes()
            .Where(type => typeof(Unit).IsAssignableFrom(type) && !type.IsAbstract)
            .ToList();

        foreach (var unitType in unitTypes)
        {
            if (GetComponent(unitType) is null) 
                continue;
            _comp = (Unit)GetComponent(unitType);
        }
    }

    private void Update()
    {
        shieldBar.fillAmount = (float)Math.Round((double)_comp.currentShield / _comp.CurrentStats.MaxHealth, 2);
    }
}
