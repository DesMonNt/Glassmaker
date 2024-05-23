using System.Collections.Generic;
using FightingScene;

namespace Effects
{
    public static class Abilities //TODO: привести всё к enum'aм
    {
        public static readonly Dictionary<string, Ability> DictOfAbilities = new()
        {
            {"Способность 1", new Ability(new () { Status.Statuses["провокация"] }, 
                new () { Status.Statuses["непровокация"] }, "Способность 1") },
            {"Ульта 1", new Ability(new (), new () { Status.Statuses["имба ебаная"] }, "Ульта 1") },
            {"Понижение защиты", new Ability(new() { Status.Statuses["дебафф брони"] },
            new(), "Понижение защиты")},
            {"Ускорение", new Ability(new() { Status.Statuses["сильное ускорение"]},
                new (), "Ускорение")},
            {
                "Плевок", new Ability(new (), new(), "Плевок")
            },
            { "Обжигающий плевок", new Ability(new() { Status.Statuses["горение"] }, new(), "горение")},
            {
                "Лечение ран", new Ability(new (), new(), "Лечение ран")
            },
            { "Повышение Макс ХП", new Ability(new() { Status.Statuses["повышение Макс ХП"]}, new(), "Повышение Макс ХП")}
        };
    }
}