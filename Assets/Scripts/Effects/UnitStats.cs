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
        public readonly bool IsImmortal;
        public readonly float CriticalChance;
        public readonly TypeOfAttack AttacksType;
        public readonly int EnergyToUlt;
        
        public UnitStats(int maxHealth, float armor, int damage,
            int speed, bool isImmortal, float criticalChance, TypeOfAttack attacksType, int energyToUlt = default)
        {
            MaxHealth = maxHealth;
            Armor = armor;
            Damage = damage;
            Speed = speed;
            IsImmortal = isImmortal;
            CriticalChance = criticalChance;
            AttacksType = attacksType;
            EnergyToUlt = energyToUlt;
        }
        
        public UnitStats(UnitStats baseStats, int maxHealth = default, float armor = default, int damage = default,
            int speed = default, bool isImmortal = default, float criticalChance = default, 
            TypeOfAttack attacksType = default, int energyToUlt = default)
        {
            MaxHealth = maxHealth == default ? baseStats.MaxHealth : maxHealth;
            Armor = Math.Abs(armor - default(float)) < 1e-5  ? baseStats.Armor : armor;
            Damage = damage == default ? baseStats.Damage : damage;
            Speed = speed == default ? baseStats.Speed : speed;
            IsImmortal = isImmortal == default ? baseStats.IsImmortal : isImmortal;
            CriticalChance = Math.Abs(criticalChance - default(float)) < 1e-5 ? baseStats.CriticalChance : criticalChance;
            AttacksType = attacksType == default ? baseStats.AttacksType : attacksType;
            EnergyToUlt = energyToUlt == default ? baseStats.EnergyToUlt : energyToUlt;
        }
    }
}