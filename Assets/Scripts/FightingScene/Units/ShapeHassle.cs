using System.Collections.Generic;
using AI;
using Effects;

namespace FightingScene.Units
{
    public class ShapeHassle: Unit
    {
        public ShapeHassle() : 
            base(new UnitStats(45000, 0.2f, 600, 125, false, 0.1f, TypeOfAttack.Single))
        {
            Brain = new BufferAI(this);
            Skill = new Ability(new List<IBuff>(), new List<IBuff> { new DamageBuff(1.15f, 2) }, "Навык");
            Ultimate = new Ability(new List<IBuff> { new ImmortalityBuff(1) }, new List<IBuff>(),
                "Эгида");
        }
    }
}