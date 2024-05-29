using System.Collections.Generic;
using AI;
using Effects;
using Unity.VisualScripting;

namespace FightingScene.Units
{
    public class AzureElemental : Unit
    {
        public AzureElemental() : base(new UnitStats(1700, 0.05f, 450, 95, false, 0.1f, TypeOfAttack.Single, 0))
        {
            Brain = new HealerAI(this);
            Skill = new Ability(new () , new List<IBuff>(), "Исцеление")
            {
                Attack = new Attack(-(int)(0.08 * CurrentStats.MaxHealth), Buffs, TypeOfAttack.Single)
            };

            Ultimate = new Ability(new List<IBuff> 
                { new MaxHPBuff((int)(0.15 * CurrentStats.MaxHealth))} , new List<IBuff>(), "Успокаивающая лазурь");
        }

        public override Ability UseAbility() => new(new(), new List<IBuff>(), "Исцеление")
        {
            Attack = new Attack(-(int)(0.08 * CurrentStats.MaxHealth), Buffs, TypeOfAttack.Single)
        };
        
        public override Ability UseUltimate() => new (new List<IBuff> 
            { new MaxHPBuff((int)(0.15 * CurrentStats.MaxHealth))} , new List<IBuff>(), "Успокаивающая лазурь");
    }
}