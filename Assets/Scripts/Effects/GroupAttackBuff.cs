using FightingScene;
using FightingScene.Units;

namespace Effects
{
    public class TypeAttackBuff : IBuff
    {
        private readonly TypeOfAttack _attacksType;
        private int _duration;
        public TypeAttackBuff(TypeOfAttack attackTypeBonus, int duration)
        {
            _attacksType = attackTypeBonus;
            _duration = duration;
        }

        public UnitStats ApplyBuff(Unit unit)
        {
            if (_duration <= 0)
                return unit.CurrentStats;
            
            _duration--;
            
            return new UnitStats(unit.CurrentStats, attacksType: _attacksType);
        }
    }
}
