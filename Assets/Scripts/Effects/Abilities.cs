using System.Collections.Generic;

namespace Effects
{
    public static class Abilities //TODO: привести всё к enum'aм
    {
        public static readonly Dictionary<string, Ability> DictOfAbilities = new()
        {
            {"Способность 1", new Ability(new () { Status.Statuses["провокация"] }, 
                new () { Status.Statuses["непровокация"] },15) },
            {"Ульта 1", new Ability(new (), new () { Status.Statuses["имба ебаная"] }, 5) }
        };
    }
}