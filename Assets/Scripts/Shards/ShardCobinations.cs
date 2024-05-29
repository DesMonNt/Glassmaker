using System.Collections.Generic;
using Effects;

public record ShardInfo
{
    public string text1, text2, text3;
}

public static class BuffInfo
{
    public static Dictionary<string, IBuff> KeyToCrimsonBuff = new()
    {
        ["+16% к урону"] = new DamageBuff(0.16f, int.MaxValue),
        ["ХУЙ"] = new DamageBuff(0.16f, int.MaxValue),
        ["Пизда"] = new DamageBuff(0.16f, int.MaxValue),
        ["Залупа"] = new DamageBuff(0.16f, int.MaxValue)
    };
    
    public static Dictionary<string, IBuff> KeyToAzureBuff = new()
    {
        ["+8% к защите"] = new DefenceBuff(0.08f, int.MaxValue),
        ["АДСКАЯ ДРОЧКА ПЫЛЕСОСОМ"] = new DamageBuff(0.16f, int.MaxValue),
        ["Малафья"] = new DamageBuff(0.16f, int.MaxValue),
        ["СПЕРМА!!!"] = new DamageBuff(0.16f, int.MaxValue)
    };

    public static Dictionary<string, IBuff> KeyToAmberBuff = new()
    {
        ["+10% к скорости"] = new SpeedBuff(0.1f, int.MaxValue),
        ["Понос"] = new DamageBuff(0.16f, int.MaxValue),
        ["Отсос"] = new DamageBuff(0.16f, int.MaxValue),
        ["Пиздапроёбина"] = new DamageBuff(0.16f, int.MaxValue)
    };

}