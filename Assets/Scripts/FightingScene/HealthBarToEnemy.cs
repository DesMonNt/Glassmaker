using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FightingScene.Units;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarToEnemy : MonoBehaviour
{
    public Image hpBar;
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
            Debug.Log($"{unitType.FullName}, {gameObject.name}");
        }
    }

    private void Update()
    {
        hpBar.fillAmount = (float)Math.Round((double)_comp.currentHealthPoints / _comp.CurrentStats.MaxHealth, 2);
    }
}