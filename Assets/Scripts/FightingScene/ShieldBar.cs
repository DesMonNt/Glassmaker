using System;
using FightingScene.Units;
using UnityEngine;
using UnityEngine.UI;

namespace FightingScene
{
    public class ShieldBar : MonoBehaviour
    {
        public Image shieldBar;
        private Unit _comp;
        private void Start() => _comp = GetComponent<Unit>();

        private void Update() => 
            shieldBar.fillAmount = (float)Math.Round((double)_comp.currentShield / _comp.CurrentStats.MaxHealth, 2);
    }
}
