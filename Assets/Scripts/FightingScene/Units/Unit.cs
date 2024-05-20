using System;
using System.Collections.Generic;
using AI;
using Effects;
using FightingScene;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class Unit : MonoBehaviour, IBuffable
{
    [SerializeField] public new string name;
    public UnitStats BaseStats;
    public UnitStats CurrentStats;

    public AI.AI Brain;
    protected List<IBuff> Buffs = new();
    
    public int currentHealthPoints;
    public int currentShield;
    public int speed;

    private FighterTurnMeter _fighterTurnMeter;

    public Ability skill;
    public Ability ultimate;

    public Unit(UnitStats baseStats)
    {
        BaseStats = baseStats;
        CurrentStats = BaseStats;
    }
    
    private void Awake() => _fighterTurnMeter = GetComponent<FighterTurnMeter>();

    private void Start()
    {
        Brain = new BufferAI(this);
        currentHealthPoints = BaseStats.MaxHealth;
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
    public virtual Ability UseAbility() => skill;
    public virtual Ability UseUltimate() => skill;
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
        CurrentStats = BaseStats;

        foreach (var buff in Buffs) 
            CurrentStats = buff.ApplyBuff(this);
    }
}
