namespace Effects
{
    public class BurnBuff : IBuff
    {
        private readonly int burnDamage;

        public BurnBuff(int burnDamage) => this.burnDamage = burnDamage;
        public UnitStats ApplyBuff(Unit unit)
        {
            var healthPointsAfterBurn = unit.currentHealthPoints - burnDamage;
            if (healthPointsAfterBurn < unit.CurrentStats.MaxHealth * 0.2)
                return unit.CurrentStats;
            unit.currentHealthPoints = healthPointsAfterBurn;
            return unit.CurrentStats;
        }
    }
}