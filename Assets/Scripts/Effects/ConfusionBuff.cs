using FightingScene.Units;

namespace Effects
{
    public class ConfusionBuff : IBuff
    {
        private int _duration;

        public ConfusionBuff(int duration)
        {
            _duration = duration;
        }

        public UnitStats ApplyBuff(Unit unit)
        {
            if (_duration <= 0)
                return unit.CurrentStats;

            _duration--;
            
            return new UnitStats(unit.CurrentStats, isConfused: true);
        }
    }
}