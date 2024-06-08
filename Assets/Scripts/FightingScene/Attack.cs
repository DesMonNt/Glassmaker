using Effects;
using Unit = FightingScene.Units.Unit;

namespace FightingScene
{
    public class Attack : IAction
    {
        public readonly int Damage;
        public readonly TypeOfAttack TypeAttack;

        public Attack(int damage, TypeOfAttack type)
        {
            Damage = damage;
            TypeAttack = type;
        }
        
        public void Execute(Unit owner, Unit target, float coefficientOfDamage)
        {
            var random = new System.Random();
            var randomValue = random.Next(0, 100);
            if (randomValue < owner.CurrentStats.CriticalChance)
                target.GetAttack((int)(2 * Damage * coefficientOfDamage));
            else
                target.GetAttack((int)(Damage * coefficientOfDamage));
        }
    }
}