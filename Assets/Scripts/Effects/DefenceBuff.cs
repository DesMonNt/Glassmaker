using System;
using FightingScene.Units;

namespace Effects
{
    public class DefenceBuff : IBuff
    {
        private readonly float _changeDefence;
        private int _duration;

        public DefenceBuff(float changeDefence, int duration)
        {
           _changeDefence = changeDefence;
           _duration = duration;
        }

        public UnitStats ApplyBuff(Unit unit)
        {
            if (_duration <= 0)
                return unit.CurrentStats;

            _duration--;
            
            var currentArmor = unit.CurrentStats.Armor;
            currentArmor = Math.Clamp(currentArmor + _changeDefence, 0, 0.9f);
            
            return new UnitStats(unit.CurrentStats, armor: currentArmor);
        }
    }
}