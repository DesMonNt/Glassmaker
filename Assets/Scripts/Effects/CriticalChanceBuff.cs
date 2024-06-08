using FightingScene.Units;

namespace Effects
{
    public class CriticalChanceBuff : IBuff
    {
        private readonly float _additionalChance;
        private int _duration;

        public CriticalChanceBuff(float criticalBonus, int duration)
        {
            _additionalChance = criticalBonus;
            _duration = duration;
        }

        public UnitStats ApplyBuff(Unit unit)
        {
            if (_duration <= 0)
                return unit.CurrentStats;

            _duration--;
            
            return new UnitStats(unit.CurrentStats,
                criticalChance: (int)(unit.CurrentStats.CriticalChance + _additionalChance));
        }
    }
}