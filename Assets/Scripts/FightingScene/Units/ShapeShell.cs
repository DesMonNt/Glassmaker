using System.Collections.Generic;
using AI;
using Effects;

namespace FightingScene.Units
{
    public class ShapeShell: Unit
    {
        public ShapeShell() : 
            base(new UnitStats(70000, 0.2f, 200, 90, false, 0.1f, TypeOfAttack.Single, 4))
        {
            Brain = new HealerAI(this);
            Skill = new Ability(new List<IBuff>(), new List<IBuff> { new ShieldBuff(7500) }, "Оберегающий панцирь");
            Ultimate = new Ability(new List<IBuff> { new DefenceBuff(0.4f, 2) }, new List<IBuff>(),
                "Лазурное укрепление");
        }
    }
}