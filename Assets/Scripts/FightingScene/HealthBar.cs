using System;
using Effects;
using UnityEngine;
using UnityEngine.UI;
using Unit = FightingScene.Units.Unit;

namespace FightingScene
{
    public class HealthBar : MonoBehaviour
    {
        public Image hpBar;
        public Image hpBarWhenLosingHp;
        private Unit _comp;

        private float _decreaseTime = 1f;
        private float _currentFillAmount;

        private void Start() => _comp = GetComponent<IBuffable>() as Unit;

        private void Update()
        {
            hpBar.fillAmount = (float)Math.Round((double)_comp.currentHealthPoints / _comp.CurrentStats.MaxHealth, 2);
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
}
