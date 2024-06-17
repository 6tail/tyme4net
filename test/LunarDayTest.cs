using tyme.lunar;

namespace test;

/// <summary>
/// 农历日测试
/// </summary>
public class LunarDayTest
{
    [Fact]
    public void Test1()
    {
        Assert.Equal("1年1月1日", LunarDay.FromYmd(0, 11, 18).GetSolarDay().ToString());
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal("9999年12月31日", LunarDay.FromYmd(9999, 12, 2).GetSolarDay().ToString());
    }

    [Fact]
    public void Test3()
    {
        Assert.Equal("1905年2月4日", LunarDay.FromYmd(1905, 1, 1).GetSolarDay().ToString());
    }

    [Fact]
    public void Test4()
    {
        Assert.Equal("2039年1月23日", LunarDay.FromYmd(2038, 12, 29).GetSolarDay().ToString());
    }

    [Fact]
    public void Test5()
    {
        Assert.Equal("1500年1月31日", LunarDay.FromYmd(1500, 1, 1).GetSolarDay().ToString());
    }

    [Fact]
    public void Test6()
    {
        Assert.Equal("1501年1月18日", LunarDay.FromYmd(1500, 12, 29).GetSolarDay().ToString());
    }

    [Fact]
    public void Test7()
    {
        Assert.Equal("1582年10月4日", LunarDay.FromYmd(1582, 9, 18).GetSolarDay().ToString());
    }

    [Fact]
    public void Test8()
    {
        Assert.Equal("1582年10月15日", LunarDay.FromYmd(1582, 9, 19).GetSolarDay().ToString());
    }

    [Fact]
    public void Test9()
    {
        Assert.Equal("2020年1月6日", LunarDay.FromYmd(2019, 12, 12).GetSolarDay().ToString());
    }

    [Fact]
    public void Test10()
    {
        Assert.Equal("2033年12月22日", LunarDay.FromYmd(2033, -11, 1).GetSolarDay().ToString());
    }

    [Fact]
    public void Test11()
    {
        Assert.Equal("2021年7月16日", LunarDay.FromYmd(2021, 6, 7).GetSolarDay().ToString());
    }

    [Fact]
    public void Test12()
    {
        Assert.Equal("2034年2月19日", LunarDay.FromYmd(2034, 1, 1).GetSolarDay().ToString());
    }

    [Fact]
    public void Test13()
    {
        Assert.Equal("2034年1月20日", LunarDay.FromYmd(2033, 12, 1).GetSolarDay().ToString());
    }

    [Fact]
    public void Test14()
    {
        Assert.Equal("7013年12月24日", LunarDay.FromYmd(7013, -11, 4).GetSolarDay().ToString());
    }

    [Fact]
    public void Test15()
    {
        Assert.Equal("己亥", LunarDay.FromYmd(2023, 8, 24).SixtyCycle.ToString());
    }

    [Fact]
    public void Test16()
    {
        Assert.Equal("癸酉", LunarDay.FromYmd(1653, 1, 6).SixtyCycle.ToString());
    }

    [Fact]
    public void Test17()
    {
        Assert.Equal("农历庚寅年二月初二", LunarDay.FromYmd(2010, 1, 1).Next(31).ToString());
    }

    [Fact]
    public void Test18()
    {
        Assert.Equal("农历壬辰年闰四月初一", LunarDay.FromYmd(2012, 3, 1).Next(60).ToString());
    }

    [Fact]
    public void Test19()
    {
        Assert.Equal("农历壬辰年闰四月廿九", LunarDay.FromYmd(2012, 3, 1).Next(88).ToString());
    }

    [Fact]
    public void Test20()
    {
        Assert.Equal("农历壬辰年五月初一", LunarDay.FromYmd(2012, 3, 1).Next(89).ToString());
    }

    [Fact]
    public void Test21()
    {
        Assert.Equal("2020年4月23日", LunarDay.FromYmd(2020, 4, 1).GetSolarDay().ToString());
    }

    [Fact]
    public void Test22()
    {
        Assert.Equal("甲辰", LunarDay.FromYmd(2024, 1, 1).Month.Year.SixtyCycle.GetName());
    }

    [Fact]
    public void Test23()
    {
        Assert.Equal("癸卯", LunarDay.FromYmd(2023, 12, 30).Month.Year.SixtyCycle.GetName());
    }

    /// <summary>
    /// 二十八宿
    /// </summary>
    [Fact]
    public void Test24()
    {
        var d = LunarDay.FromYmd(2020, 4, 13);
        var star = d.TwentyEightStar;
        Assert.Equal("南", star.Zone.GetName());
        Assert.Equal("朱雀", star.Zone.GetBeast().GetName());
        Assert.Equal("翼", star.GetName());
        Assert.Equal("火", star.SevenStar.GetName());
        Assert.Equal("蛇", star.GetAnimal().GetName());
        Assert.Equal("凶", star.Luck.GetName());

        Assert.Equal("阳天", star.Land.GetName());
        Assert.Equal("东南", star.Land.GetDirection().GetName());
    }

    [Fact]
    public void Test25()
    {
        var d = LunarDay.FromYmd(2023, 9, 28);
        var star = d.TwentyEightStar;
        Assert.Equal("南", star.Zone.GetName());
        Assert.Equal("朱雀", star.Zone.GetBeast().GetName());
        Assert.Equal("柳", star.GetName());
        Assert.Equal("土", star.SevenStar.GetName());
        Assert.Equal("獐", star.GetAnimal().GetName());
        Assert.Equal("凶", star.Luck.GetName());

        Assert.Equal("炎天", star.Land.GetName());
        Assert.Equal("南", star.Land.GetDirection().GetName());
    }

    [Fact]
    public void Test26()
    {
        var lunar = LunarDay.FromYmd(2005, 11, 23);
        Assert.Equal("戊子", lunar.Month.SixtyCycle.GetName());
        Assert.Equal("戊子", lunar.MonthSixtyCycle.GetName());
    }
}