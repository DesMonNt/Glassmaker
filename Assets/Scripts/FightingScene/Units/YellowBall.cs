using Effects;

namespace FightingScene.Units
{
    public class YellowBall: Unit
    {
        public YellowBall() : 
            base(new UnitStats(25000, 0.05f, 750, 105, false, 0.1f, TypeOfAttack.Single))
        {
            skill = Abilities.DictOfAbilities["Понижение защиты"];
            ultimate = Abilities.DictOfAbilities["Ускорение"];
        }
    }
}