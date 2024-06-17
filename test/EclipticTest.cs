using tyme.solar;

namespace test;

/// <summary>
/// 黄道黑道十二神测试
/// </summary>
public class EclipticTest
{
    [Fact]
    public void Test0()
    {
        var star = SolarDay.FromYmd(2023, 10, 30).GetLunarDay().TwelveStar;
        Assert.Equal("天德", star.GetName());
        Assert.Equal("黄道", star.Ecliptic.GetName());
        Assert.Equal("吉", star.Ecliptic.Luck.GetName());
    }

    [Fact]
    public void Test1()
    {
        var star = SolarDay.FromYmd(2023, 10, 19).GetLunarDay().TwelveStar;
        Assert.Equal("白虎", star.GetName());
        Assert.Equal("黑道", star.Ecliptic.GetName());
        Assert.Equal("凶", star.Ecliptic.Luck.GetName());
    }

    [Fact]
    public void Test2()
    {
        var star = SolarDay.FromYmd(2023, 10, 7).GetLunarDay().TwelveStar;
        Assert.Equal("天牢", star.GetName());
        Assert.Equal("黑道", star.Ecliptic.GetName());
        Assert.Equal("凶", star.Ecliptic.Luck.GetName());
    }

    [Fact]
    public void Test3()
    {
        var star = SolarDay.FromYmd(2023, 10, 8).GetLunarDay().TwelveStar;
        Assert.Equal("玉堂", star.GetName());
        Assert.Equal("黄道", star.Ecliptic.GetName());
        Assert.Equal("吉", star.Ecliptic.Luck.GetName());
    }
}