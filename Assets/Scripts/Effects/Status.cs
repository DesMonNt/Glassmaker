using System.Collections.Generic;
using Effects;

public static class Status
{
    public static Dictionary<string, IBuff> Statuses = new()
    {
        { "провокация", new AttackBuff(20) },
        { "Невосприимчивость", new ImmortalityBuff() },
        // {
        //     "3 скилл Оракла", target =>
        //     {
        //         target.currentHealthPoints -= target.HealthPoints * 0.4f;
        //         target.currentHealthPoints += target.HealthPoints * 0.6f;
        //     }
        // },
        {
            "Аура на бафф атаки", new DamageBuff(0.15f)
        }
    };
}
