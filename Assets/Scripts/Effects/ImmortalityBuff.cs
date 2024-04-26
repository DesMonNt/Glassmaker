namespace Effects
{
    public class ImmortalityBuff : IBuff
    {
        public UnitStats ApplyBuff(UnitStats baseStats)
        {
            var newStats = baseStats;
            newStats.IsImmortal = true;
            return newStats;
        }
    }
}