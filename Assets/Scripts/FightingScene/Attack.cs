using System;
using System.Collections.Generic;
using System.Linq;
using Effects;
using JetBrains.Annotations;

namespace FightingScene
{
    public class Attack
    {
        public int Damage;
        [CanBeNull] public IEnumerable<IBuff> AttackEffects; // single / group / Aoe

        public Attack(int damage, [CanBeNull] IEnumerable<IBuff> buffs)
        {
            Damage = damage;
            AttackEffects = buffs;
        }

        public void Execute(Unit target)
        {
            target.GetAttack(Damage);
            if (AttackEffects is null)
                return;
            foreach (var buff in AttackEffects)
            {
                target.AddBuff(buff);
            }
        }
    }
}