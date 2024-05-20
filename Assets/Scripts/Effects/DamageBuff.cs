namespace Effects
{
    public class DamageBuff : IBuff
    {
        private readonly float _damageBonus;

        public DamageBuff(float damageBonus) => _damageBonus = damageBonus;

        public UnitStats ApplyBuff(UnitStats baseStats) 
            => new(baseStats, (int)(baseStats.Damage + baseStats.Damage * _damageBonus));
    }
}