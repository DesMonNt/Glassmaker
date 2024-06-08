using FightingScene.Units;

namespace Effects
{
    public interface IAction
    {
        public void Execute(Unit owner, Unit target, float coefficientOfDamage);
    }
}