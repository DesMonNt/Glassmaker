using System.Collections.Generic;
using AI;
using Effects;

namespace FightingScene.Units
{
    public class ShapeTormentor: Unit
    {
        public ShapeTormentor() : 
            base(new UnitStats(5000, 0.2f, 700, 85, false, 0.1f, TypeOfAttack.Single, 0))
        {
            Brain = new DamageDealerAI(this);
            Skill = new Ability(new List<IBuff>(),
                new List<IBuff> { new TypeAttackBuff(TypeOfAttack.Aoe, 1), new AttackBuff(100) }, "Ярость Мучителя");
            Ultimate = new Ability(new List<IBuff> {new BurnBuff((int)(CurrentStats.Damage * 0.35), 3)}, 
                new List<IBuff> { new DamageBuff(1.07f, 2) }, "Обжигающий багрянец")
            {
                Attack = new Attack((int)(CurrentStats.Damage * 0.9), Buffs, TypeOfAttack.Aoe)
            };
        }
    }
}