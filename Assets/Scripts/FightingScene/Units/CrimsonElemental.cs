using AI;
using Effects;

namespace FightingScene.Units
{
    public class CrimsonElemental : Unit
    {
        public CrimsonElemental() : base(new UnitStats(30000, 0.2f, 1000, 115, false, 0.1f, TypeOfAttack.Single, 0))
        {
            Brain = new DamageDealerAI(this);
            Skill = new Ability(new (), new(), "Плевок")
            {
                Attack = new Attack(800, Buffs, TypeOfAttack.Single)
            };
            Ultimate = new Ability(new() { new BurnBuff(250, 5) }, new(), "горение");
        }
    }
}