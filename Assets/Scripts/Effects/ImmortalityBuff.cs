namespace Effects
{
    public class ImmortalityBuff : IBuff
    {
        public UnitStats ApplyBuff(Unit unit) => new(unit.CurrentStats, isImmortal: true);
    }
}