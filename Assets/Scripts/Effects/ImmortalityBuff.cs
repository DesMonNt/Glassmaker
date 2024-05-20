namespace Effects
{
    public class ImmortalityBuff : IBuff
    {
        public UnitStats ApplyBuff(UnitStats baseStats) => new(baseStats, isImmortal: true);
    }
}