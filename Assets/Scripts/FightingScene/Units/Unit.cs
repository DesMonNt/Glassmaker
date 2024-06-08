using System;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using UnityEngine.Events;

namespace FightingScene.Units
{
    public abstract class Unit : MonoBehaviour
    {
        public Sprite attackSprite;
        public Sprite skillSprite;
        public Sprite ultimateSprite;

        public AudioClip attackSound;
                                 
        public new string name;
        public UnitStats CurrentStats;

        public AI.AI Brain;
        private readonly List<IBuff> _buffs = new();
    
        public int currentHealthPoints;
        public int currentShield;
        public int speed;

        public Ability Skill;
        public Ability Ultimate;
    
        private readonly UnitStats _baseStats;
        private FighterTurnMeter _fighterTurnMeter;

        protected Unit(UnitStats baseStats)
        {
            _baseStats = baseStats;
            CurrentStats = _baseStats;
        }
    
        private void Awake() => _fighterTurnMeter = GetComponent<FighterTurnMeter>();

        private void Start()
        {
            currentHealthPoints = _baseStats.MaxHealth;
        }

        public void IncreaseTurnMeter()
        {
            _fighterTurnMeter.Increase();

            if (_fighterTurnMeter.CanOffensive)
                TurnMeterFilled?.Invoke(this);
        }
        
        /// <summary>
        /// Принимает на вход урон, нанесённый атакующим. Далее в методе просчитывается урон, который получит
        /// цель с учётом всех баффов/дебаффов, брони и щита, которые на ней висят
        /// </summary>
        public virtual void GetAttack(int damage)
        {
            if (damage < 0)
            {
                currentHealthPoints = Math.Clamp(currentHealthPoints - damage, 0, CurrentStats.MaxHealth);
                return;
            }
            
            if (CurrentStats.IsImmortal)
                return;
            
            switch (currentShield)
            {
                case 0:
                    currentHealthPoints -= (int)(damage * (1 - CurrentStats.Armor));
                    break;
                default:
                {
                    var delta = (int)(currentShield - damage * (1 - CurrentStats.Armor));
                    currentShield = Math.Clamp(delta, 0, Math.Abs(delta));
                
                    if (delta < 0)
                        currentHealthPoints += delta;
                    break;
                }
            }
            
            if (currentHealthPoints <= 0)
                GetDied();
        }

        public Attack UseAttack() => new (CurrentStats.Damage, CurrentStats.AttacksType);
        public virtual Ability UseAbility() => Skill;
        public virtual Ability UseUltimate() => Ultimate;

        public event UnityAction<Unit> TurnMeterFilled;
        public event UnityAction<Unit> Died;

        private void GetDied() => Died?.Invoke(this);

        public void AddBuff(IBuff buff)
        {
            _buffs.Add(buff);
        
            ApplyBuffs();
        }

        public void RemoveAllBuffs() => _buffs.Clear();

        public void ApplyBuffs()
        {
            CurrentStats = _baseStats;

            foreach (var buff in _buffs) 
                CurrentStats = buff.ApplyBuff(this);
        }
    }
}
