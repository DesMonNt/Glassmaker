using FightingScene.Units;

namespace Effects
{
    public class ShieldBuffWithDuration : IBuff
    {
        private readonly int _shieldValue;
        private int _duration;

        public ShieldBuffWithDuration(int shieldValue, int duration)
        {
            _shieldValue = shieldValue;
            _duration = duration;
        }

        public UnitStats ApplyBuff(Unit unit)
        {
            if (_duration <= 0)
                return unit.CurrentStats;

            _duration--;
        
            unit.currentShield += _shieldValue;
            
            return unit.CurrentStats;
        }
    }
}
