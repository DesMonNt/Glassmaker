using Effects;
using System.Collections.Generic;

public static class BuffConverter
{
    public static Dictionary<string, IBuff> NameToBuff = new()
    {
        ["critical"] = new DamageBuff(0.16f),
        ["speed"] = new SpeedBuff(0.1f),
    };
}