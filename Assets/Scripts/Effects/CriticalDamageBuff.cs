using FightingScene.Units;

namespace Effects
{
    public class CriticalDamageBuff : IBuff
    {
        private readonly float _additionalCriticalDamage;
        private int _duration;

        public CriticalDamageBuff(float criticalBonus, int duration)
        {
            _additionalCriticalDamage = criticalBonus;
            _duration = duration;
        }

        public UnitStats ApplyBuff(Unit unit)
        {
            if (_duration <= 0)
                return unit.CurrentStats;

            _duration--;
            
            return new UnitStats(unit.CurrentStats,
                damage: (int)(unit.CurrentStats.Damage + _additionalCriticalDamage));
        }
    }
}