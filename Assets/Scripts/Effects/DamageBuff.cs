using FightingScene.Units;

namespace Effects
{
    public class DamageBuff : IBuff
    {
        private readonly float _damageBonus;
        private int _duration;

        public DamageBuff(float damageBonus, int duration)
        {
            _damageBonus = damageBonus;
            _duration = duration;
        }

        public UnitStats ApplyBuff(Unit unit)
        {
            if (_duration <= 0)
                return unit.CurrentStats;

            _duration--;
            
            return new UnitStats(unit.CurrentStats, (int)(unit.CurrentStats.Damage + unit.CurrentStats.Damage * _damageBonus));
        }
    }
}