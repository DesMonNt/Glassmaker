using UnityEngine;

namespace Effects
{
    public class AttackBuff : IBuff
    {
        private readonly int _damageBonus;

        public AttackBuff(int damageBonus) => _damageBonus = damageBonus;

        public UnitStats ApplyBuff(UnitStats baseStats) => new(baseStats, damage: baseStats.Damage + _damageBonus);
    }
}