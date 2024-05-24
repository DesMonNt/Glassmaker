using AI;
using Effects;

namespace FightingScene.Units
{
    public class AmberElemental: Unit
    {
        public AmberElemental() : 
            base(new UnitStats(25000, 0.05f, 750, 105, false, 0.1f, TypeOfAttack.Single, 0))
        {
            Brain = new BufferAI(this);
            Skill = Abilities.DictOfAbilities["Понижение защиты"];
            Ultimate = Abilities.DictOfAbilities["Ускорение"];
        }
    }
}