using FightingScene;

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
        public TypeOfAttack AttacksType;
        public int Shield;
        
        public UnitStats(int maxHealth, float armor, int damage,
            int speed, int energy, bool isImmortal, float criticalChance, TypeOfAttack attacksType, int shield)
        {
            MaxHealth = maxHealth;
            Armor = armor;
            Damage = damage;
            Speed = speed;
            Energy = energy;
            IsImmortal = isImmortal;
            CriticalChance = criticalChance;
            AttacksType = attacksType;
            Shield = shield;
        }
    }
    
    
}