namespace Effects
{
    public class UnitStats
    {
        public int MaxHealth;
        public float Armor;
        public int Damage;
        public int Speed;
        public int Energy;
        public bool IsImmortal;
        public float CriticalChance;
        
        public UnitStats(int maxHealth, float armor, int damage,
            int speed, int energy, bool isImmortal, float criticalChance)
        {
            MaxHealth = maxHealth;
            Armor = armor;
            Damage = damage;
            Speed = speed;
            Energy = energy;
            IsImmortal = isImmortal;
            CriticalChance = criticalChance;
        }
    }
    
    
}