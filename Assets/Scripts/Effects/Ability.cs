using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Ability : MonoBehaviour
{
    public float damageFromAbility;
    public string nameme;
    [FormerlySerializedAs("goodStatuses")] public List<string> statuses;

    public Ability(string title, List<string> ss, float damage)
    {
        nameme = title;
        statuses = ss;
        damageFromAbility = damage;
    }

    public void Execute(Unit target)
    {
        target.GetMagicAttack(damageFromAbility);
        foreach (var status in statuses) 
            target.AddBuff(Status.Statuses[status]);
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
