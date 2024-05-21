using System.Collections.Generic;
using AI;
using Effects;

namespace FightingScene.Units
{
    public class ShapeTormentor: Unit
    {
        public ShapeTormentor() : 
            base(new UnitStats(65000, 0.2f, 1200, 95, false, 0.1f, TypeOfAttack.Single))
        {
            Brain = new DamageDealerAI(this);
            Skill = new Ability(new List<IBuff>(),
                new List<IBuff> { new TypeAttackBuff(TypeOfAttack.Group, 1), new AttackBuff(550) }, "Ярость Мучителя");
            Ultimate = new Ability(new List<IBuff> {new BurnBuff(250, 3)}, new List<IBuff>(), "Обжигающий багрянец")
            {
                Attack = new Attack(1000, Buffs, TypeOfAttack.Aoe)
            };
        }
    }
}