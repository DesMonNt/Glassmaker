using System.Collections.Generic;
using AI;
using Effects;

namespace FightingScene.Units
{
    public class CrimsonElemental : Unit
    {
        public CrimsonElemental() : 
            base(new UnitStats(2200, 0.2f, 600, 115, false, 
                0.1f, TypeOfAttack.Single, 0))
        {
            Brain = new DamageDealerAI(this);
            Skill = new Ability(new List<IBuff>(), new List<IBuff>(), "Багровый шип")
            {
                Attack = new Attack((int)(CurrentStats.Damage * 1.25), TypeOfAttack.Single)
            };
            Ultimate = new Ability(
                new List<IBuff> { new BurnBuff((int)(CurrentStats.Damage * 0.35), 3) }, 
                new List<IBuff>(), "Колющая рана");
        }
        
        public override Ability UseAbility() => 
            new (new List<IBuff>(), new List<IBuff>(), "Багровый шип")
        {
            Attack = new Attack((int)(CurrentStats.Damage * 1.25), TypeOfAttack.Single)
        };
        
        public override Ability UseUltimate() => 
            new (new List<IBuff> { new BurnBuff((int)(CurrentStats.Damage * 0.35), 3) }, 
                new List<IBuff>(), "Колющая рана");
    }
}