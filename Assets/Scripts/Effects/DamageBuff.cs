namespace Effects
{
    public class DamageBuff : IBuff
    {
        private readonly float _damageBonus;

        public DamageBuff(float damageBonus) => _damageBonus = damageBonus;

        public UnitStats ApplyBuff(Unit unit) 
            => new(unit.CurrentStats, (int)(unit.CurrentStats.Damage + unit.CurrentStats.Damage * _damageBonus));
    }
}