using Effects;

namespace FightingScene.Units
{
    public class BlueBall : Unit
    {
        public BlueBall() : base(new UnitStats(20000, 0.05f, 450, 100, false, 0.1f, TypeOfAttack.Single))
        {
            skill = Abilities.DictOfAbilities["Лечение ран"];
            ultimate = Abilities.DictOfAbilities["Повышение Макс ХП"];
        }
    }
}