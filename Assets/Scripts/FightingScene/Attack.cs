﻿using System.Collections.Generic;
using Effects;
using FightingScene.Units;
using JetBrains.Annotations;
using UnityEngine;

namespace FightingScene
{
    public class Attack :IAction
    {
        public int Damage;
        [CanBeNull] public IEnumerable<IBuff> AttackEffects;
        public TypeOfAttack TypeAttack;

        public Attack(int damage, [CanBeNull] IEnumerable<IBuff> buffs, TypeOfAttack type)
        {
            Damage = damage;
            AttackEffects = buffs;
            TypeAttack = type;
        }

        public void Execute(Unit owner, Unit target)
        {
            var random = new System.Random();
            var randomValue = random.Next(0, 100);
            if (randomValue < owner.CurrentStats.CriticalChance)
            {
                target.GetAttack(2 * Damage);
            }
                
            else 
                target.GetAttack(Damage);
        }
        
        public void Execute(Unit owner, Unit target, float coefficientOfDamage)
        {
            var random = new System.Random();
            var randomValue = random.Next(0, 100);
            if (randomValue < owner.CurrentStats.CriticalChance)
                target.GetAttack((int)(2 * Damage * coefficientOfDamage));
            else 
                target.GetAttack((int)(Damage * coefficientOfDamage));
        }
    }
}