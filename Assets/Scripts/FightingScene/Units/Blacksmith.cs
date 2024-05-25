using System;
using System.Collections.Generic;
using Effects;

namespace FightingScene.Units
{
    public class Blacksmith : Unit
    {
        public Blacksmith() : base(new UnitStats(4000, 0.2f, 300, 100, false, 0.1f, TypeOfAttack.Single, 3))
        {
            Skill = new Ability(new List<IBuff> { new ShieldBuff((int)(0.17 * CurrentStats.MaxHealth)) }, new List<IBuff>(), "Лазурная защита", Targets.Character);
            Ultimate = new Ability(new List<IBuff> { new ShieldHealBuff(0.09f, 3) }, new List<IBuff>(), "Исцеляющий щит", Targets.Character);
        }

        public override void GetAttack(int damage)
        {
            base.GetAttack(damage);

            if (new Random().NextDouble() > 0.4)
                currentShield += (int)(0.04 * damage);
        }
    }
}