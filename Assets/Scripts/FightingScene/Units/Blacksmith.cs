using System;
using System.Collections.Generic;
using Effects;

namespace FightingScene.Units
{
    public class Blacksmith : Unit
    {
        public Blacksmith() : base(new UnitStats(4000, 0.2f, 100, 100, false, 0.1f, TypeOfAttack.Single, 3))
        {
            Skill = new Ability(new List<IBuff> { new ShieldBuff((int)(0.67 * CurrentStats.MaxHealth)) }, new List<IBuff>(), "Лазурная защита");
            Ultimate = new Ability(new List<IBuff> { new ShieldHealBuff(0.09f, 3) }, new List<IBuff>(), "Исцеляющий щит");
        }

        public override void GetAttack(int damage)
        {
            base.GetAttack(damage);

            if (new Random().NextDouble() > 0.4)
                currentShield += (int)(0.34 * damage);
        }
        
        public override Ability UseAbility() => Skill;

        public override Ability UseUltimate() => Ultimate;
    }
}