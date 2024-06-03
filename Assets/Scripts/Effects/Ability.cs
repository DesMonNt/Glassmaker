using System.Collections.Generic;
using Effects;
using FightingScene;
using JetBrains.Annotations;
using Unit = FightingScene.Units.Unit;

public class Ability : IAction
{
    private readonly List<IBuff> _ownerStatuses;
    private readonly List<IBuff> _targetStatuses;
    public readonly string Name;
    [CanBeNull] public Attack Attack;
    public readonly Targets Target;

    public Ability(List<IBuff> targetS, List<IBuff> ownerS, string name, Targets target = default)
    {
        Target = target;
        Name = name;
        _targetStatuses = targetS;
        _ownerStatuses = ownerS;
    }
    
    public void Execute(Unit owner, Unit target)
    {
        foreach (var status in _targetStatuses) 
            target.AddBuff(status);
        foreach (var status in _ownerStatuses) 
            owner.AddBuff(status);
    }
}
