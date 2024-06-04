using System.Collections.Generic;
using AI;
using Effects;

namespace FightingScene.Units
{
    public class ShapeShell: Unit
    {
        public ShapeShell() : 
            base(new UnitStats(7000, 0.2f, 200, 90, false, 
                0.1f, TypeOfAttack.Single, 0))
        {
            Brain = new HealerAI(this);
            Skill = new Ability(new List<IBuff>(), 
                new List<IBuff> { new ShieldBuff((int)(0.13 * CurrentStats.MaxHealth)) }, 
                "Оберегающий панцирь");
            Ultimate = new Ability(new List<IBuff> { new DefenceBuff(0.4f, 2) }, new List<IBuff>(),
                "Лазурное укрепление");
        }
        
        public override Ability UseAbility() => new (new List<IBuff>(),
            new List<IBuff> { new ShieldBuff((int)(0.13 * CurrentStats.MaxHealth)) }, 
            "Оберегающий панцирь");
        
        public override Ability UseUltimate() => new (new List<IBuff> { new DefenceBuff(0.4f, 2) },
            new List<IBuff>(),
            "Лазурное укрепление");

    }
}