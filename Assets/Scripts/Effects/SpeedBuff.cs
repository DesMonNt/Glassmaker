using FightingScene.Units;

namespace Effects
{
    public class SpeedBuff : IBuff
    {
        private readonly float _additionalSpeed;
        private int _duration;

        public SpeedBuff(float additionalSpeed, int duration)
        {
            _additionalSpeed = additionalSpeed;
            _duration = duration;
        }

        public UnitStats ApplyBuff(Unit unit)
        {
            if (_duration <= 0)
                return unit.CurrentStats;

            _duration--;
            
            return new UnitStats(unit.CurrentStats,
                speed: (int)(unit.CurrentStats.Speed + unit.CurrentStats.Speed * _additionalSpeed));
        }
    }
}