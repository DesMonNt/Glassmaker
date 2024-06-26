﻿using System.Collections.Generic;
using AI;
using Effects;

namespace FightingScene.Units
{
    public class AzureElemental : Unit
    {
        public AzureElemental() : base(new UnitStats(1700, 0.05f, 450, 95, false, 0.1f, TypeOfAttack.Single, 0))
        {
            Brain = new HealerAI(this);
            Skill = new Ability(new () , new List<IBuff>(), "Исцеление")
            {
                Attack = new Attack(-(int)(0.24 * CurrentStats.MaxHealth), TypeOfAttack.Single)
            };

            Ultimate = new Ability(new List<IBuff> 
                { new MaxHpBuff((int)(0.15 * CurrentStats.MaxHealth))} , new List<IBuff>(), "Успокаивающая лазурь");
        }

        public override Ability UseAbility() => Skill;
        
        public override Ability UseUltimate() => Ultimate;
    }
}