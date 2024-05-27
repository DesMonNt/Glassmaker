using System.Collections.Generic;
using AI;
using Effects;

namespace FightingScene.Units
{
    public class ShapeHassle: Unit
    {
        public ShapeHassle() : 
            base(new UnitStats(4500, 0.2f, 300, 125, false, 0.1f, TypeOfAttack.Single, 0))
        {
            Brain = new BufferAI(this);
            Skill = new Ability(new List<IBuff>(), new List<IBuff> { new DamageBuff(1.15f, 2) }, "Усиливающий янтарь");
            Ultimate = new Ability(new List<IBuff> { new ImmortalityBuff(1) }, new List<IBuff>(), "Нерушимость");
        }
        
        public override Ability UseAbility() =>  new (new List<IBuff>(),
            new List<IBuff> { new DamageBuff(1.15f, 2) }, "Усиливающий янтарь");

        public override Ability UseUltimate() => 
            new(new List<IBuff> { new ImmortalityBuff(1) }, new List<IBuff>(), "Нерушимость");
    }
}