using FightingScene.Units;

namespace Effects
{
    public class MaxHPBuff : IBuff
    {
        private readonly int _maxHpBonus;

        public MaxHPBuff(int maxHpBonus) => _maxHpBonus = maxHpBonus;

        public UnitStats ApplyBuff(Unit unit)
        {
            unit.currentHealthPoints += _maxHpBonus;
            
            return new UnitStats(unit.CurrentStats, unit.CurrentStats.MaxHealth + _maxHpBonus);
        }
    }
}