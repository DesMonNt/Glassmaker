using System.Collections.Generic;
using AI;
using Effects;

namespace FightingScene.Units
{
    public class AmberElemental: Unit
    {
        public AmberElemental() : 
            base(new UnitStats(2000, 0.05f, 350, 105, false, 0.1f, TypeOfAttack.Single, 0))
        {
            Brain = new BufferAI(this);
            Skill = new Ability(new List<IBuff> { new DefenceBuff(1.1f, 5) }, new List<IBuff>(), "Повышение защиты");
            Ultimate = new Ability(new List<IBuff> { new SpeedBuff(1.15f, 3) }, new List<IBuff>(), "Ускорение частиц");
        }
    }
}