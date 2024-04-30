using UnityEngine;

namespace Effects
{
    public class AttackBuff : IBuff
    {
        private readonly int _damageBonus;

        public AttackBuff(int damageBonus) => _damageBonus = damageBonus;

        public UnitStats ApplyBuff(UnitStats baseStats)
        {
            var newStats = baseStats;
            newStats.Damage = Mathf.Max(newStats.Damage + _damageBonus, 0);
            return newStats;
        }
    }
}