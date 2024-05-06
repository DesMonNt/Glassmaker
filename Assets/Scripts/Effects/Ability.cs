using System.Collections.Generic;
using Effects;

public class Ability: IAction
{
    private readonly int _damageFromAbility;
    private readonly List<IBuff> _ownerStatuses;
    private readonly List<IBuff> _targetStatuses;

    public Ability(List<IBuff> ts, List<IBuff> os, int damage)
    {
        _targetStatuses = ts;
        _ownerStatuses = os;
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
    //
    // public void UseAbility(Unit target)
    // {
    //     
    //     foreach (var titleOfGoodStatus in statuses)
    //         target.UnitGoodStatuses.Add(titleOfGoodStatus);
    //     foreach (var titleOfBadStatus in badStatuses) 
    //         target.UnitBadStatuses.Add(titleOfBadStatus);
    //     target.GetMagicAttack(damageFromAbility);
    // }
}
