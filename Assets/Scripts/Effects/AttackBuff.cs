using UnityEngine;

namespace Effects
{
    public class AttackBuff : IBuff
    {
        private readonly int _damageBonus;

        public AttackBuff(int damageBonus) => _damageBonus = damageBonus;

        public UnitStats ApplyBuff(Unit unit) => new(unit.CurrentStats, damage: unit.CurrentStats.Damage + _damageBonus);
    }
}