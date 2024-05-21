using FightingScene.Units;

namespace Effects
{
    public class BurnBuff : IBuff
    {
        private readonly int _burnDamage;
        private int _duration;

        public BurnBuff(int burnDamage, int duration)
        {
            _burnDamage = burnDamage;
            _duration = duration;
        }

        public UnitStats ApplyBuff(Unit unit)
        {
            if (_duration <= 0)
                return unit.CurrentStats;

            _duration--;
            unit.currentHealthPoints -= _burnDamage;
            
            return unit.CurrentStats;
        }
    }
}