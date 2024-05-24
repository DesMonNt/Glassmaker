using System;
using FightingScene.Units;

namespace Effects
{
    public class HealingBuff : IBuff
    {
        private readonly int _hpBonus;
        private int _duration;

        public HealingBuff(int hpBonus, int duration)
        {
            _hpBonus = hpBonus;
            _duration = duration;
        }

        public UnitStats ApplyBuff(Unit unit)
        {
            if (_duration <= 0)
                return unit.CurrentStats;

            _duration--;
            unit.currentHealthPoints = Math.Clamp(unit.currentHealthPoints + _hpBonus, 0, unit.CurrentStats.MaxHealth);
            
            return unit.CurrentStats;
        }
    }
}