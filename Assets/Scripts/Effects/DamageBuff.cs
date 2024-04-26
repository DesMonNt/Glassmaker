using System;
using UnityEngine;

namespace Effects
{
    public class DamageBuff : IBuff
    {
        private readonly float _damageBonus;

        public DamageBuff(float damageBonus) => _damageBonus = damageBonus;

        public UnitStats ApplyBuff(UnitStats baseStats)
        {
            var newStats = baseStats;
            newStats.Damage += (int)(newStats.Damage * _damageBonus);
            return newStats;
        }
    }
}