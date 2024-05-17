using System.Collections.Generic;
using AI;
using Effects;
using FightingScene;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Unit : MonoBehaviour, IBuffable
{
    [SerializeField] public new string name;
    public UnitStats BaseStats = 
        new (100, 0.2f, 20, 1999, 500, false, 0.15f, TypeOfAttack.Single);
    public UnitStats CurrentStats = 
        new (100, 0.2f, 20, 1999, 500, false, 0.15f, TypeOfAttack.Single);

    // public Unit(UnitStats baseStats)
    // {
    //     BaseStats = baseStats;
    //     CurrentStats = BaseStats;
    // }

    public BufferAI Brain;
    private readonly List<IBuff> _buffs = new();
    
    [FormerlySerializedAs("healthPoints")] public float currentHealthPoints;
    public float baseDamage;

    public Sprite spritePassive;
    public Sprite spriteActive;
    [FormerlySerializedAs("spirtRenderer")] public SpriteRenderer spriteRenderer;
    
    public int speed;
    public float armor;
    public int energy;
    public int energyToUltimate;

    private FighterTurnMeter _fighterTurnMeter;

    public string skill;
    public string ultimate;
    public string passive;

    private void Awake() => _fighterTurnMeter = GetComponent<FighterTurnMeter>();

    private void Start()
    {
        Brain = new BufferAI(this);
        //AddBuff(Status.Statuses[passive]);
        currentHealthPoints = BaseStats.MaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    public Attack UseAttack() => new (CurrentStats.Damage, _buffs, CurrentStats.AttacksType);
    public Ability UseAbility() => Abilities.DictOfAbilities[skill];
    public Ability UseUltimate() => Abilities.DictOfAbilities[ultimate];
    public void GetMagicAttack(float damage)
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
        _buffs.Add(buff);
        
        ApplyBuffs();
    }

    public void RemoveBuff(IBuff buff)
    {
        _buffs.Remove(buff);
        ApplyBuffs();
    }

    private void ApplyBuffs()
    {
        CurrentStats = BaseStats;

        foreach (var buff in _buffs) 
            CurrentStats = buff.ApplyBuff(CurrentStats);
    }
}
