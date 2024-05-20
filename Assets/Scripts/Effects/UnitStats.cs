using System;
using FightingScene;

namespace Effects
{
    public class UnitStats
    {
        public readonly int MaxHealth;
        public readonly float Armor;
        public readonly int Damage;
        public readonly int Speed;
        public readonly int Energy;
        public readonly bool IsImmortal;
        public readonly float CriticalChance;
        public readonly TypeOfAttack AttacksType;
        
        public UnitStats(int maxHealth, float armor, int damage,
            int speed, int energy, bool isImmortal, float criticalChance, TypeOfAttack attacksType)
        {
            MaxHealth = maxHealth;
            Armor = armor;
            Damage = damage;
            Speed = speed;
            Energy = energy;
            IsImmortal = isImmortal;
            CriticalChance = criticalChance;
            AttacksType = attacksType;
        }
        
        public UnitStats(UnitStats baseStats, int maxHealth = default, float armor = default, int damage = default,
            int speed = default, int energy = default, bool isImmortal = default, float criticalChance = default, 
            TypeOfAttack attacksType = default)
        {
            MaxHealth = maxHealth == default ? baseStats.MaxHealth : maxHealth;
            Armor = Math.Abs(armor - default(float)) < 1e-5  ? baseStats.Armor : armor;
            Damage = damage == default ? baseStats.Damage : damage;
            Speed = speed == default ? baseStats.Speed : speed;
            Energy = energy == default ? baseStats.Energy : energy;
            IsImmortal = isImmortal == default ? baseStats.IsImmortal : isImmortal;
            CriticalChance = Math.Abs(criticalChance - default(float)) < 1e-5 ? baseStats.CriticalChance : criticalChance;
            AttacksType = attacksType == default ? baseStats.AttacksType : attacksType;
        }
    }
    
    
}