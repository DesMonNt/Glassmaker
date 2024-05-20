using System;

namespace Effects
{
    public class DefenceBuff : IBuff
    {
        private readonly float changeDefence;

        public DefenceBuff(float changeDefence) => this.changeDefence = changeDefence;
        public UnitStats ApplyBuff(Unit unit)
        {
            var currentArmor = unit.CurrentStats.Armor;
            currentArmor = Math.Clamp(0, currentArmor + changeDefence, 1);
            return new(unit.CurrentStats, armor: currentArmor);
        }
    }
}