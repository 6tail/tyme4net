using tyme.lunar;

namespace test;

/// <summary>
/// 时辰测试
/// </summary>
public class LunarHourTest
{
    [Fact]
    public void Test1()
    {
        LunarHour h = LunarHour.FromYmdHms(2020, -4, 5, 23, 0, 0);
        Assert.Equal("子时", h.GetName());
        Assert.Equal("农历庚子年闰四月初五戊子时", h.ToString());
    }

    [Fact]
    public void Test2()
    {
        LunarHour h = LunarHour.FromYmdHms(2020, -4, 5, 0, 59, 0);
        Assert.Equal("子时", h.GetName());
        Assert.Equal("农历庚子年闰四月初五丙子时", h.ToString());
    }

    [Fact]
    public void Test3()
    {
        LunarHour h = LunarHour.FromYmdHms(2020, -4, 5, 1, 0, 0);
        Assert.Equal("丑时", h.GetName());
        Assert.Equal("农历庚子年闰四月初五丁丑时", h.ToString());
    }

    [Fact]
    public void Test4()
    {
        LunarHour h = LunarHour.FromYmdHms(2020, -4, 5, 21, 30, 0);
        Assert.Equal("亥时", h.GetName());
        Assert.Equal("农历庚子年闰四月初五丁亥时", h.ToString());
    }

    [Fact]
    public void Test5()
    {
        LunarHour h = LunarHour.FromYmdHms(2020, -4, 2, 23, 30, 0);
        Assert.Equal("子时", h.GetName());
        Assert.Equal("农历庚子年闰四月初二壬子时", h.ToString());
    }

    [Fact]
    public void Test6()
    {
        LunarHour h = LunarHour.FromYmdHms(2020, 4, 28, 23, 30, 0);
        Assert.Equal("子时", h.GetName());
        Assert.Equal("农历庚子年四月廿八甲子时", h.ToString());
    }

    [Fact]
    public void Test7()
    {
        LunarHour h = LunarHour.FromYmdHms(2020, 4, 29, 0, 0, 0);
        Assert.Equal("子时", h.GetName());
        Assert.Equal("农历庚子年四月廿九甲子时", h.ToString());
    }

    [Fact]
    public void Test8()
    {
        LunarHour h = LunarHour.FromYmdHms(2023, 11, 14, 23, 0, 0);
        Assert.Equal("甲子", h.SixtyCycle.GetName());

        Assert.Equal("己未", h.DaySixtyCycle.GetName());
        Assert.Equal("戊午", h.Day.SixtyCycle.GetName());
        Assert.Equal("农历癸卯年十一月十四", h.Day.ToString());

        Assert.Equal("甲子", h.MonthSixtyCycle.GetName());
        Assert.Equal("农历癸卯年十一月", h.Day.Month.ToString());
        Assert.Equal("乙丑", h.Day.Month.SixtyCycle.GetName());

        Assert.Equal("癸卯", h.YearSixtyCycle.GetName());
        Assert.Equal("农历癸卯年", h.Day.Month.Year.ToString());
        Assert.Equal("癸卯", h.Day.Month.Year.SixtyCycle.GetName());
    }

    [Fact]
    public void Test9()
    {
        LunarHour h = LunarHour.FromYmdHms(2023, 11, 14, 6, 0, 0);
        Assert.Equal("乙卯", h.SixtyCycle.GetName());

        Assert.Equal("戊午", h.DaySixtyCycle.GetName());
        Assert.Equal("戊午", h.Day.SixtyCycle.GetName());
        Assert.Equal("农历癸卯年十一月十四", h.Day.ToString());

        Assert.Equal("甲子", h.MonthSixtyCycle.GetName());
        Assert.Equal("农历癸卯年十一月", h.Day.Month.ToString());
        Assert.Equal("乙丑", h.Day.Month.SixtyCycle.GetName());

        Assert.Equal("癸卯", h.YearSixtyCycle.GetName());
        Assert.Equal("农历癸卯年", h.Day.Month.Year.ToString());
        Assert.Equal("癸卯", h.Day.Month.Year.SixtyCycle.GetName());
    }
}