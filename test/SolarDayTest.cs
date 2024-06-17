using tyme.solar;

namespace test;

/// <summary>
/// 公历日测试
/// </summary>
public class SolarDayTest
{
    [Fact]
    public void Test0()
    {
        Assert.Equal("1日", SolarDay.FromYmd(2023, 1, 1).GetName());
        Assert.Equal("2023年1月1日", SolarDay.FromYmd(2023, 1, 1).ToString());
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal("29日", SolarDay.FromYmd(2000, 2, 29).GetName());
        Assert.Equal("2000年2月29日", SolarDay.FromYmd(2000, 2, 29).ToString());
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal(0, SolarDay.FromYmd(2023, 1, 1).IndexInYear);
        Assert.Equal(364, SolarDay.FromYmd(2023, 12, 31).IndexInYear);
        Assert.Equal(365, SolarDay.FromYmd(2020, 12, 31).IndexInYear);
    }

    [Fact]
    public void Test3()
    {
        Assert.Equal(0, SolarDay.FromYmd(2023, 1, 1).Subtract(SolarDay.FromYmd(2023, 1, 1)));
        Assert.Equal(1, SolarDay.FromYmd(2023, 1, 2).Subtract(SolarDay.FromYmd(2023, 1, 1)));
        Assert.Equal(-1, SolarDay.FromYmd(2023, 1, 1).Subtract(SolarDay.FromYmd(2023, 1, 2)));
        Assert.Equal(31, SolarDay.FromYmd(2023, 2, 1).Subtract(SolarDay.FromYmd(2023, 1, 1)));
        Assert.Equal(-31, SolarDay.FromYmd(2023, 1, 1).Subtract(SolarDay.FromYmd(2023, 2, 1)));
        Assert.Equal(365, SolarDay.FromYmd(2024, 1, 1).Subtract(SolarDay.FromYmd(2023, 1, 1)));
        Assert.Equal(-365, SolarDay.FromYmd(2023, 1, 1).Subtract(SolarDay.FromYmd(2024, 1, 1)));
        Assert.Equal(1, SolarDay.FromYmd(1582, 10, 15).Subtract(SolarDay.FromYmd(1582, 10, 4)));
    }

    [Fact]
    public void Test4()
    {
        Assert.Equal("1582年10月4日", SolarDay.FromYmd(1582, 10, 15).Next(-1).ToString());
    }

    [Fact]
    public void Test5()
    {
        Assert.Equal("2000年3月1日", SolarDay.FromYmd(2000, 2, 28).Next(2).ToString());
    }

    [Fact]
    public void Test6()
    {
        Assert.Equal("农历庚子年闰四月初二", SolarDay.FromYmd(2020, 5, 24).GetLunarDay().ToString());
    }

    [Fact]
    public void Test7()
    {
        Assert.Equal(31, SolarDay.FromYmd(2020, 5, 24).Subtract(SolarDay.FromYmd(2020, 4, 23)));
    }

    [Fact]
    public void Test8()
    {
        Assert.Equal("农历丙子年十一月十二", SolarDay.FromYmd(16, 11, 30).GetLunarDay().ToString());
    }

    [Fact]
    public void Test9()
    {
        Assert.Equal("霜降", SolarDay.FromYmd(2023, 10, 27).Term.ToString());
    }

    [Fact]
    public void Test10()
    {
        Assert.Equal("豺乃祭兽第4天", SolarDay.FromYmd(2023, 10, 27).PhenologyDay.ToString());
    }

    [Fact]
    public void Test11()
    {
        Assert.Equal("初候", SolarDay.FromYmd(2023, 10, 27).PhenologyDay.Phenology.ThreePhenology.ToString());
    }

    [Fact]
    public void Test22()
    {
        Assert.Equal("甲辰", SolarDay.FromYmd(2024, 2, 10).GetLunarDay().Month.Year.SixtyCycle.GetName());
    }

    [Fact]
    public void Test23()
    {
        Assert.Equal("癸卯", SolarDay.FromYmd(2024, 2, 9).GetLunarDay().Month.Year.SixtyCycle.GetName());
    }
}