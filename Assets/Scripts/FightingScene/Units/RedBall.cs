using Effects;

namespace FightingScene.Units
{
    public class RedBall : Unit
    {
        public RedBall() : base(new UnitStats(30000, 0.2f, 1000, 115, false, 0.1f, TypeOfAttack.Single))
        {
            skill = Abilities.DictOfAbilities["Плевок"];
            ultimate = Abilities.DictOfAbilities["Обжигающий плевок"];
        }
    }
}