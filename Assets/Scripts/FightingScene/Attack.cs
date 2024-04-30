using System;
using System.Collections.Generic;
using System.Linq;
using Effects;
using JetBrains.Annotations;
using Random = UnityEngine.Random;

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

        public void Execute(Unit target, float criticalChance)
        {
            var random = new System.Random();
            var randomValue = random.Next(0, 100);
            if (randomValue < criticalChance)
                target.GetAttack(2 * Damage);
            else 
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