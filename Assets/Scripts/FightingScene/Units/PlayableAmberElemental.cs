using System.Collections.Generic;
using Effects;

namespace FightingScene.Units
{
    public class PlayableAmberElemental : Unit
    {
        public PlayableAmberElemental() : base(new UnitStats(4200, 0.2f, 200, 135, false, 0.1f, TypeOfAttack.Single, 3))
        {
            Skill = new Ability(new List<IBuff> { new SpeedBuff(1.17f, 3), new DamageBuff(1.42f, 2) }, new List<IBuff>(), "Янтарный пакт", Targets.Character);
            Ultimate = new Ability(new List<IBuff> { new ConfusionBuff(4) }, new List<IBuff>(), "Замешательство",
                Targets.Enemy);
        }
        
        public override Ability UseAbility() => new (new List<IBuff> 
            { new SpeedBuff(1.17f, 3), new DamageBuff(1.42f, 2) },
            new List<IBuff>(), "Янтарный пакт", Targets.Character);
        
        public override Ability UseUltimate() =>  new (new List<IBuff>
            {
                new ConfusionBuff(4)
            }, new List<IBuff>(), "Замешательство",
            Targets.Enemy);

    }
}