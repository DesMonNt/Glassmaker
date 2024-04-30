using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarToEnemy : MonoBehaviour
{
    public Image hpBar;
    private Enemy _comp;
    private void Start() => _comp = GetComponent<Enemy>();

    private void Update() => hpBar.fillAmount = _comp.currentHealthPoints / _comp.CurrentStats.MaxHealth;
}