using tyme.lunar;
using tyme.solar;

namespace test;

/// <summary>
/// 农历月测试
/// </summary>
public class LunarMonthTest
{
    [Fact]
    public void Test0()
    {
        Assert.Equal("七月", LunarMonth.FromYm(2359, 7).GetName());
    }

    /// <summary>
    /// 闰月
    /// </summary>
    [Fact]
    public void Test1()
    {
        Assert.Equal("闰七月", LunarMonth.FromYm(2359, -7).GetName());
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal(29, LunarMonth.FromYm(2023, 6).DayCount);
    }

    [Fact]
    public void Test3()
    {
        Assert.Equal(30, LunarMonth.FromYm(2023, 7).DayCount);
    }

    [Fact]
    public void Test4()
    {
        Assert.Equal(30, LunarMonth.FromYm(2023, 8).DayCount);
    }

    [Fact]
    public void Test5()
    {
        Assert.Equal(29, LunarMonth.FromYm(2023, 9).DayCount);
    }

    [Fact]
    public void Test6()
    {
        Assert.Equal("2023年10月15日", LunarMonth.FromYm(2023, 9).FirstJulianDay.GetSolarDay().ToString());
    }

    [Fact]
    public void Test7()
    {
        Assert.Equal("甲寅", LunarMonth.FromYm(2023, 1).SixtyCycle.GetName());
    }

    [Fact]
    public void Test8()
    {
        Assert.Equal("丙辰", LunarMonth.FromYm(2023, -2).SixtyCycle.GetName());
    }

    [Fact]
    public void Test9()
    {
        Assert.Equal("丁巳", LunarMonth.FromYm(2023, 3).SixtyCycle.GetName());
    }

    [Fact]
    public void Test10()
    {
        Assert.Equal("丙寅", LunarMonth.FromYm(2024, 1).SixtyCycle.GetName());
    }

    [Fact]
    public void Test11()
    {
        Assert.Equal("丙寅", LunarMonth.FromYm(2023, 12).SixtyCycle.GetName());
    }

    [Fact]
    public void Test12()
    {
        Assert.Equal("壬寅", LunarMonth.FromYm(2022, 1).SixtyCycle.GetName());
    }

    [Fact]
    public void Test13()
    {
        Assert.Equal("闰十二月", LunarMonth.FromYm(37, -12).GetName());
    }

    [Fact]
    public void Test14()
    {
        Assert.Equal("闰十二月", LunarMonth.FromYm(5552, -12).GetName());
    }

    [Fact]
    public void Test15()
    {
        Assert.Equal("农历戊子年十二月", LunarMonth.FromYm(2008, 11).Next(1).ToString());
    }

    [Fact]
    public void Test16()
    {
        Assert.Equal("农历己丑年正月", LunarMonth.FromYm(2008, 11).Next(2).ToString());
    }

    [Fact]
    public void Test17()
    {
        Assert.Equal("农历己丑年五月", LunarMonth.FromYm(2008, 11).Next(6).ToString());
    }

    [Fact]
    public void Test18()
    {
        Assert.Equal("农历己丑年闰五月", LunarMonth.FromYm(2008, 11).Next(7).ToString());
    }

    [Fact]
    public void Test19()
    {
        Assert.Equal("农历己丑年六月", LunarMonth.FromYm(2008, 11).Next(8).ToString());
    }

    [Fact]
    public void Test20()
    {
        Assert.Equal("农历庚寅年正月", LunarMonth.FromYm(2008, 11).Next(15).ToString());
    }

    [Fact]
    public void Test21()
    {
        Assert.Equal("农历戊子年十一月", LunarMonth.FromYm(2008, 12).Next(-1).ToString());
    }

    [Fact]
    public void Test22()
    {
        Assert.Equal("农历戊子年十一月", LunarMonth.FromYm(2009, 1).Next(-2).ToString());
    }

    [Fact]
    public void Test23()
    {
        Assert.Equal("农历戊子年十一月", LunarMonth.FromYm(2009, 5).Next(-6).ToString());
    }

    [Fact]
    public void Test24()
    {
        Assert.Equal("农历戊子年十一月", LunarMonth.FromYm(2009, -5).Next(-7).ToString());
    }

    [Fact]
    public void Test25()
    {
        Assert.Equal("农历戊子年十一月", LunarMonth.FromYm(2009, 6).Next(-8).ToString());
    }

    [Fact]
    public void Test26()
    {
        Assert.Equal("农历戊子年十一月", LunarMonth.FromYm(2010, 1).Next(-15).ToString());
    }

    [Fact]
    public void Test27()
    {
        Assert.Equal(29, LunarMonth.FromYm(2012, -4).DayCount);
    }

    [Fact]
    public void Test28()
    {
        Assert.Equal("癸亥", LunarMonth.FromYm(2023, 9).SixtyCycle.ToString());
    }

    [Fact]
    public void Test29()
    {
        LunarDay d = SolarDay.FromYmd(2023, 10, 7).GetLunarDay();
        Assert.Equal("壬戌", d.LunarMonth.SixtyCycle.ToString());
        Assert.Equal("辛酉", d.MonthSixtyCycle.ToString());
    }

    [Fact]
    public void Test30()
    {
        LunarDay d = SolarDay.FromYmd(2023, 10, 8).GetLunarDay();
        Assert.Equal("壬戌", d.LunarMonth.SixtyCycle.ToString());
        Assert.Equal("壬戌", d.MonthSixtyCycle.ToString());
    }

    [Fact]
    public void Test31()
    {
        LunarDay d = SolarDay.FromYmd(2023, 10, 15).GetLunarDay();
        Assert.Equal("九月", d.LunarMonth.GetName());
        Assert.Equal("癸亥", d.LunarMonth.SixtyCycle.ToString());
        Assert.Equal("壬戌", d.MonthSixtyCycle.ToString());
    }

    [Fact]
    public void Test32()
    {
        LunarDay d = SolarDay.FromYmd(2023, 11, 7).GetLunarDay();
        Assert.Equal("癸亥", d.LunarMonth.SixtyCycle.ToString());
        Assert.Equal("壬戌", d.MonthSixtyCycle.ToString());
    }

    [Fact]
    public void Test33()
    {
        LunarDay d = SolarDay.FromYmd(2023, 11, 8).GetLunarDay();
        Assert.Equal("癸亥", d.LunarMonth.SixtyCycle.ToString());
        Assert.Equal("癸亥", d.MonthSixtyCycle.ToString());
    }

    [Fact]
    public void Test34()
    {
        // 2023年闰2月
        LunarMonth m = LunarMonth.FromYm(2023, 12);
        Assert.Equal("农历癸卯年十二月", m.ToString());
        Assert.Equal("农历癸卯年十一月", m.Next(-1).ToString());
        Assert.Equal("农历癸卯年十月", m.Next(-2).ToString());
    }

    [Fact]
    public void Test35()
    {
        // 2023年闰2月
        LunarMonth m = LunarMonth.FromYm(2023, 3);
        Assert.Equal("农历癸卯年三月", m.ToString());
        Assert.Equal("农历癸卯年闰二月", m.Next(-1).ToString());
        Assert.Equal("农历癸卯年二月", m.Next(-2).ToString());
        Assert.Equal("农历癸卯年正月", m.Next(-3).ToString());
        Assert.Equal("农历壬寅年十二月", m.Next(-4).ToString());
        Assert.Equal("农历壬寅年十一月", m.Next(-5).ToString());
    }

    [Fact]
    public void Test36()
    {
        LunarDay d = SolarDay.FromYmd(1983, 2, 15).GetLunarDay();
        Assert.Equal("甲寅", d.LunarMonth.SixtyCycle.ToString());
        Assert.Equal("甲寅", d.MonthSixtyCycle.ToString());
    }

    [Fact]
    public void Test37()
    {
        LunarDay d = SolarDay.FromYmd(2023, 10, 30).GetLunarDay();
        Assert.Equal("癸亥", d.LunarMonth.SixtyCycle.ToString());
        Assert.Equal("壬戌", d.MonthSixtyCycle.ToString());
    }

    [Fact]
    public void Test38()
    {
        LunarDay d = SolarDay.FromYmd(2023, 10, 19).GetLunarDay();
        Assert.Equal("癸亥", d.LunarMonth.SixtyCycle.ToString());
        Assert.Equal("壬戌", d.MonthSixtyCycle.ToString());
    }

    [Fact]
    public void Test39()
    {
        LunarMonth m = LunarMonth.FromYm(2023, 11);
        Assert.Equal("农历癸卯年十一月", m.ToString());
        Assert.Equal("乙丑", m.SixtyCycle.ToString());
    }

    [Fact]
    public void Test40()
    {
        Assert.Equal("庚申", LunarDay.FromYmd(2018, 6, 26).MonthSixtyCycle.ToString());
    }

    [Fact]
    public void Test41()
    {
        Assert.Equal("辛丑", LunarMonth.FromYm(1991, 12).SixtyCycle.ToString());
    }
}