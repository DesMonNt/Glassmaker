using FightingScene.Units;
using UnityEditor;
using UnityEngine;

namespace Effects
{
    public class ShieldBuff: IBuff
    {
        private readonly int _shieldValue;
        private bool _isUsed;

        public ShieldBuff(int shieldValue) => _shieldValue = shieldValue;
        public UnitStats ApplyBuff(Unit unit)
        {
            if (_isUsed)
                return unit.CurrentStats;
            
            unit.currentShield += _shieldValue;
            _isUsed = true;
            
            Debug.Log($"Наложил: {_shieldValue}");
            
            return unit.CurrentStats;
        }
    }
}