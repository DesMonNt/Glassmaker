using System;
using System.Collections;
using System.Collections.Generic;
using Effects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Unit = FightingScene.Units.Unit;

public class HealthBar : MonoBehaviour
{
    public Image hpBar;
    public Image hpBarWhenLosingHp;
    private Unit comp;

    private float _decreaseTime = 1f;
    private float _currentFillAmount;
    void Start()
    {
        comp = GetComponent<IBuffable>() as Unit;
    }

    private void Update()
    {
        hpBar.fillAmount = (float)Math.Round((double)comp.currentHealthPoints / comp.CurrentStats.MaxHealth, 2);
        if (hpBarWhenLosingHp.fillAmount - hpBar.fillAmount > -0.001)
        {
            if (_decreaseTime < 1)
                _decreaseTime += Time.deltaTime;
            else
            {
                hpBarWhenLosingHp.fillAmount -= Math.Abs((_currentFillAmount - hpBar.fillAmount) / 480);
            }
        }
            
        else
        {
            _decreaseTime = 0;
            _currentFillAmount = hpBar.fillAmount;
        }
    }
}
