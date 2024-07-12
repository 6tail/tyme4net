using tyme.solar;

namespace test;

/// <summary>
/// 宜忌测试
/// </summary>
public class TabooTest
{
    [Fact]
    public void Test0()
    {
        var taboos = SolarDay.FromYmd(2024, 6, 26).GetLunarDay().Recommends.Select(t => t.GetName()).ToList();

        Assert.Equal(new List<string>
        {
            "嫁娶", "祭祀", "理发", "作灶", "修饰垣墙", "平治道涂", "整手足甲", "沐浴", "冠笄"
        }, taboos);
    }

    [Fact]
    public void Test1()
    {
        var taboos = SolarDay.FromYmd(2024, 6, 26).GetLunarDay().Avoids.Select(t => t.GetName()).ToList();

        Assert.Equal(new List<string>
        {
            "破土", "出行", "栽种"
        }, taboos);
    }

    [Fact]
    public void Test2()
    {
        var taboos = SolarTime.FromYmdHms(2024, 4, 22, 0, 0, 0).GetLunarHour().Recommends.Select(t => t.GetName())
            .ToList();

        Assert.Equal(new List<string>
        {
            "嫁娶", "交易", "开市", "安床", "祭祀", "求财"
        }, taboos);
    }

    [Fact]
    public void Test3()
    {
        var taboos = SolarTime.FromYmdHms(2024, 4, 22, 0, 0, 0).GetLunarHour().Avoids.Select(t => t.GetName()).ToList();

        Assert.Equal(new List<string>
        {
            "出行", "移徙", "赴任", "词讼", "祈福", "修造", "求嗣"
        }, taboos);
    }
}