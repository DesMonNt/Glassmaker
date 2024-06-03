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
        ["+10% к урону"] = new DamageBuff(0.1f, int.MaxValue),
        ["Очень горячий осколок"] = new BurnBuff(50, 2),
        ["+8% к шансу крит. урона"] = new CriticalChanceBuff(0.08f, int.MaxValue),
        ["+400 к крит. урону"] = new CriticalDamageBuff(400, int.MaxValue)
    };
    
    public static Dictionary<string, IBuff> KeyToAzureBuff = new()
    {
        ["+8% к защите"] = new DefenceBuff(0.08f, int.MaxValue),
        ["+12% к защите"] = new DefenceBuff(0.12f, int.MaxValue),
        ["+375 к щиту"] = new ShieldBuffWithDuration(125, 4),
        ["+750 к щиту"] = new ShieldBuffWithDuration(250, 4)
    };

    public static Dictionary<string, IBuff> KeyToAmberBuff = new()
    {
        ["+10% к скорости"] = new SpeedBuff(0.1f, int.MaxValue),
        ["+25% к скорости"] = new SpeedBuff(0.25f, int.MaxValue),
        ["+500 к макс. здоровью"] = new MaxHPBuff(500),
        ["+100 к макс. здоровью"] = new MaxHPBuff(100)
    };

}