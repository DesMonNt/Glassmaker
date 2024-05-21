using AI;
using Effects;

namespace FightingScene.Units
{
    public class AzureElemental : Unit
    {
        public AzureElemental() : base(new UnitStats(20000, 0.05f, 450, 100, false, 0.1f, TypeOfAttack.Single))
        {
            Brain = new HealerAI(this);
            Skill = Abilities.DictOfAbilities["Лечение ран"];
            Ultimate = Abilities.DictOfAbilities["Повышение Макс ХП"];
        }
    }
}