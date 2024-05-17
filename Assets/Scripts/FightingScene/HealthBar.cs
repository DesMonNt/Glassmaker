using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpBar;
    public Image hpBarRed;
    private Character comp;
    private float _currentFillAmount;
    private float _decreaseTime;
    void Start()
    {
        comp = GetComponent<Character>();
        _currentFillAmount = hpBar.fillAmount;
    }

    private void Update()
    {
        hpBar.fillAmount = comp.currentHealthPoints / comp.CurrentStats.MaxHealth;
        if (hpBarRed.fillAmount - hpBar.fillAmount > 0.01)
        {
            if (_decreaseTime < 1)
                _decreaseTime += Time.deltaTime;
            else
            {
                hpBarRed.fillAmount -= (_currentFillAmount - hpBar.fillAmount) / 480;
                hpBarRed.color = Color.black;
            }
        }
            
        else
        {
            _decreaseTime = 0;
            _currentFillAmount = hpBar.fillAmount;
            hpBarRed.color = Color.red;

        }
    }
}
