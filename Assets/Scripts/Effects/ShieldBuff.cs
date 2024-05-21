using FightingScene.Units;

namespace Effects
{
    public class ShieldBuff: IBuff
    {
        private readonly int _shieldValue;

        public ShieldBuff(int shieldValue) => _shieldValue = shieldValue;
        public UnitStats ApplyBuff(Unit unit)
        {
            unit.currentShield += _shieldValue;
            
            return new UnitStats(unit.CurrentStats);
        }
    }
}