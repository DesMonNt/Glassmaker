using System.Collections.Generic;
using Effects;
using FightingScene;

public static class Status // TODO: все скиллы переписать + enum'ы
{
    public static Dictionary<string, IBuff> Statuses = new()
    {
        { "провокация", new AttackBuff(20) },
        { "непровокация", new TypeAttackBuff(TypeOfAttack.Aoe) },
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
        },
        { "имба ебаная", new DamageBuff(100)},
        { "дебафф брони", new DefenceBuff(-0.1f) },
        { "сильное ускорение", new SpeedBuff(1.15f) },
        { "горение", new BurnBuff(250) },
        { "повышение Макс ХП", new MaxHPBuff(10000) }
    };
}
