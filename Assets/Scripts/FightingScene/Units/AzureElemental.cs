using System.Collections.Generic;
using AI;
using Effects;
using UnityEngine;

namespace FightingScene.Units
{
    public class AzureElemental : Unit
    {
        public AzureElemental() : base(new UnitStats(20000, 0.05f, 450, 100, false, 0.1f, TypeOfAttack.Single, 0))
        {
            Brain = new HealerAI(this);
            Skill = new Ability(new () , new List<IBuff>(), "Лечение ран")
            {
                Attack = new Attack(-9000, Buffs, TypeOfAttack.Single)
            };

            Ultimate = new Ability(new List<IBuff>() { new MaxHPBuff(10000)} , new List<IBuff>(), "Больше хп");
        }
    }
}