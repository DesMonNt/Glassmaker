namespace Effects
{
    public class SpeedBuff : IBuff
    {
        private readonly float _additionalSpeed;

        public SpeedBuff(float additionalSpeed) => _additionalSpeed = additionalSpeed;
        
        public UnitStats ApplyBuff(Unit unit) 
            => new(unit.CurrentStats, speed: (int)(unit.CurrentStats.Speed + unit.CurrentStats.Speed * _additionalSpeed));
    }
}