using FightingScene.Units;

namespace Effects
{
    public class ShieldHealBuff: IBuff
    {
        private readonly float _healingPercent;
        private int _duration;

        public ShieldHealBuff(float healingPercent, int duration)
        {
            _healingPercent = healingPercent;
            _duration = duration;
        }

        public UnitStats ApplyBuff(Unit unit)
        {
            if (_duration <= 0 || unit.currentShield <= 0)
                return unit.CurrentStats;

            _duration--;

            unit.currentHealthPoints += (int)(_healingPercent * unit.CurrentStats.MaxHealth);
            
            return unit.CurrentStats;
        }
    }
}