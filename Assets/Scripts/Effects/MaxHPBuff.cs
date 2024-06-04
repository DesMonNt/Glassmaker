using FightingScene.Units;

namespace Effects
{
    public class MaxHpBuff : IBuff
    {
        private readonly int _maxHpBonus;

        public MaxHpBuff(int maxHpBonus) => _maxHpBonus = maxHpBonus;

        public UnitStats ApplyBuff(Unit unit)
        {
            unit.currentHealthPoints += _maxHpBonus;
            
            return new UnitStats(unit.CurrentStats, unit.CurrentStats.MaxHealth + _maxHpBonus);
        }
    }
}