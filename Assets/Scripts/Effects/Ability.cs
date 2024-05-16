using System.Collections.Generic;
using Effects;

public class Ability : IAction
{
    private readonly int _damageFromAbility;
    private readonly List<IBuff> _ownerStatuses;
    private readonly List<IBuff> _targetStatuses;

    public Ability(List<IBuff> targetS, List<IBuff> ownerS, int damage)
    {
        _targetStatuses = targetS;
        _ownerStatuses = ownerS;
        _damageFromAbility = damage;
    }
    
    public void Execute(Unit owner, Unit target)
    {
        target.GetMagicAttack(_damageFromAbility);
        foreach (var status in _targetStatuses) 
            target.AddBuff(status);
        foreach (var status in _ownerStatuses) 
            owner.AddBuff(status);
    }
}
