using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpBar;
    private Character comp;
    void Start()
    {
        comp = GetComponent<Character>();
    }

    private void Update()
    {
        hpBar.fillAmount = comp.currentHealthPoints / comp.CurrentStats.MaxHealth;
    }
}
