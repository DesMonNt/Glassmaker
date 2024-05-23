using FightingScene.Units;

namespace Effects
{
    public interface IBuff
    {
        UnitStats ApplyBuff(Unit unit);
    }
}