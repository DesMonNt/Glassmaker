using FightingScene;

namespace Effects
{
    public class TypeAttackBuff : IBuff
    {
        private readonly TypeOfAttack _attacksType;

        public TypeAttackBuff(TypeOfAttack attackTypeBonus) => _attacksType = attackTypeBonus;
        public UnitStats ApplyBuff(UnitStats baseStats) => new(baseStats, attacksType: _attacksType);
    }
}
