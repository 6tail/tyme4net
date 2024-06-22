using tyme.solar;

namespace test;

/// <summary>
/// 梅雨天测试
/// </summary>
public class PlumRainDayTest
{
    [Fact]
    public void Test0()
    {
        var d = SolarDay.FromYmd(2024, 6, 10).PlumRainDay;
        Assert.Null(d);
    }

    [Fact]
    public void Test1()
    {
        var d = SolarDay.FromYmd(2024, 6, 11).PlumRainDay;
        Assert.Equal("入梅", d.GetName());
        Assert.Equal("入梅", d.PlumRain.ToString());
        Assert.Equal("入梅第1天", d.ToString());
    }

    [Fact]
    public void Test2()
    {
        var d = SolarDay.FromYmd(2024, 7, 6).PlumRainDay;
        Assert.Equal("出梅", d.GetName());
        Assert.Equal("出梅", d.PlumRain.ToString());
        Assert.Equal("出梅", d.ToString());
    }

    [Fact]
    public void Test3()
    {
        var d = SolarDay.FromYmd(2024, 7, 5).PlumRainDay;
        Assert.Equal("入梅", d.GetName());
        Assert.Equal("入梅", d.PlumRain.ToString());
        Assert.Equal("入梅第25天", d.ToString());
    }
}