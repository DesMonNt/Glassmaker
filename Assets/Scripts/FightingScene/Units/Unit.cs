using System.Collections.Generic;
using AI;
using Effects;
using UnityEngine;
using UnityEngine.Events;

namespace FightingScene.Units
{
    public abstract class Unit : MonoBehaviour, IBuffable
    {
        [SerializeField] 
        public new string name;
        public UnitStats CurrentStats;

        public AI.AI Brain;
        protected readonly List<IBuff> Buffs = new();
    
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
            Brain = new BufferAI(this);
            currentHealthPoints = _baseStats.MaxHealth;
        }

        public void IncreaseTurnMeter()
        {
            _fighterTurnMeter.Increase();

            if (_fighterTurnMeter.CanOffensive)
                TurnMeterFilled?.Invoke(this);
        }

        public void GetAttack(int damage)
        {
            currentHealthPoints -= (int)(damage * (1 - CurrentStats.Armor));
            if (currentHealthPoints <= 0)
                GetDied();
        }

        public virtual Attack UseAttack() => new (CurrentStats.Damage, Buffs, CurrentStats.AttacksType);
        public virtual Ability UseAbility() => Skill;
        public virtual Ability UseUltimate() => Skill;
        public void GetMagicAttack(int damage)
        {
            currentHealthPoints -= damage;
            if (currentHealthPoints <= 0)
                GetDied();
        }

        public event UnityAction<Unit> TurnMeterFilled;
        public event UnityAction<Unit> Died;

        public void GetDied() => Died?.Invoke(this);

        public void AddBuff(IBuff buff)
        {
            Buffs.Add(buff);
        
            ApplyBuffs();
        }

        public void RemoveBuff(IBuff buff)
        {
            Buffs.Remove(buff);
            ApplyBuffs();
        }

        private void ApplyBuffs()
        {
            CurrentStats = _baseStats;

            foreach (var buff in Buffs) 
                CurrentStats = buff.ApplyBuff(this);
        }
    }
}
