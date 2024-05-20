namespace Effects
{
    public class SpeedBuff : IBuff
    {
        private readonly float _additionalSpeed;

        public SpeedBuff(float additionalSpeed) => _additionalSpeed = additionalSpeed;
        
        public UnitStats ApplyBuff(UnitStats baseStats) 
            => new(baseStats, speed: (int)(baseStats.Speed + baseStats.Speed * _additionalSpeed));
    }
}