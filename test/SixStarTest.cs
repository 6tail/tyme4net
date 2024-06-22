using tyme.solar;

namespace test;

/// <summary>
/// 六曜测试
/// </summary>
public class SixStarTest
{
    [Fact]
    public void Test0()
    {
        Assert.Equal("佛灭", SolarDay.FromYmd(2020, 4, 23).GetLunarDay().SixStar.GetName());
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal("友引", SolarDay.FromYmd(2021, 1, 15).GetLunarDay().SixStar.GetName());
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal("先胜", SolarDay.FromYmd(2017, 1, 5).GetLunarDay().SixStar.GetName());
    }

    [Fact]
    public void Test3()
    {
        Assert.Equal("友引", SolarDay.FromYmd(2020, 4, 10).GetLunarDay().SixStar.GetName());
    }

    [Fact]
    public void Test4()
    {
        Assert.Equal("大安", SolarDay.FromYmd(2020, 6, 11).GetLunarDay().SixStar.GetName());
    }

    [Fact]
    public void Test5()
    {
        Assert.Equal("先胜", SolarDay.FromYmd(2020, 6, 1).GetLunarDay().SixStar.GetName());
    }

    [Fact]
    public void Test6()
    {
        Assert.Equal("先负", SolarDay.FromYmd(2020, 12, 8).GetLunarDay().SixStar.GetName());
    }

    [Fact]
    public void Test8()
    {
        Assert.Equal("赤口", SolarDay.FromYmd(2020, 12, 11).GetLunarDay().SixStar.GetName());
    }
}