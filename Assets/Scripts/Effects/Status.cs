using System.Collections.Generic;
using Effects;
using FightingScene;

public static class Status // TODO: все скиллы переписать + enum'ы
{
    public static Dictionary<string, IBuff> Statuses = new()
    {
        { "провокация", new AttackBuff(20) },
        { "непровокация", new TypeAttackBuff(TypeOfAttack.Aoe, 1) },
        { "Невосприимчивость", new ImmortalityBuff(1) },
        {
            "Аура на бафф атаки", new DamageBuff(0.15f, 1)
        },
        { "имба ебаная", new DamageBuff(100, 1)},
        { "дебафф брони", new DefenceBuff(-0.1f, 2) },
        { "сильное ускорение", new SpeedBuff(1.15f, 2) },
        { "горение", new BurnBuff(250, 1) },
        { "повышение Макс ХП", new MaxHPBuff(10000) }
    };
}
