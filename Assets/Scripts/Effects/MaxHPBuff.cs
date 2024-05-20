namespace Effects
{
    public class MaxHPBuff : IBuff
    {
        private readonly int _maxHpBonus;

        public MaxHPBuff(int maxHpBonus) => _maxHpBonus = maxHpBonus;

        public UnitStats ApplyBuff(Unit unit) 
            => new(unit.CurrentStats, unit.CurrentStats.MaxHealth + _maxHpBonus);
    }
}