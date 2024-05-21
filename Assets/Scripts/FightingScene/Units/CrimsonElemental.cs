using AI;
using Effects;

namespace FightingScene.Units
{
    public class CrimsonElemental : Unit
    {
        public CrimsonElemental() : base(new UnitStats(30000, 0.2f, 1000, 115, false, 0.1f, TypeOfAttack.Single))
        {
            Brain = new DamageDealerAI(this);
            Skill = Abilities.DictOfAbilities["Плевок"];
            Ultimate = Abilities.DictOfAbilities["Обжигающий плевок"];
        }
    }
}