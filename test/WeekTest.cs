using tyme.lunar;
using tyme.solar;

namespace test;

/// <summary>
/// 星期测试
/// </summary>
public class WeekTest
{
    [Fact]
    public void Test0()
    {
        Assert.Equal("一", SolarDay.FromYmd(1582, 10, 1).Week.GetName());
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal("五", SolarDay.FromYmd(1582, 10, 15).Week.GetName());
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal(2, SolarDay.FromYmd(2023, 10, 31).Week.Index);
    }

    [Fact]
    public void Test3()
    {
        SolarWeek w = SolarWeek.FromYm(2023, 10, 0, 0);
        Assert.Equal("第一周", w.GetName());
        Assert.Equal("2023年10月第一周", w.ToString());
    }

    [Fact]
    public void Test5()
    {
        SolarWeek w = SolarWeek.FromYm(2023, 10, 4, 0);
        Assert.Equal("第五周", w.GetName());
        Assert.Equal("2023年10月第五周", w.ToString());
    }

    [Fact]
    public void Test6()
    {
        SolarWeek w = SolarWeek.FromYm(2023, 10, 5, 1);
        Assert.Equal("第六周", w.GetName());
        Assert.Equal("2023年10月第六周", w.ToString());
    }

    [Fact]
    public void Test7()
    {
        SolarWeek w = SolarWeek.FromYm(2023, 10, 0, 0).Next(4);
        Assert.Equal("第五周", w.GetName());
        Assert.Equal("2023年10月第五周", w.ToString());
    }

    [Fact]
    public void Test8()
    {
        SolarWeek w = SolarWeek.FromYm(2023, 10, 0, 0).Next(5);
        Assert.Equal("第二周", w.GetName());
        Assert.Equal("2023年11月第二周", w.ToString());
    }

    [Fact]
    public void Test9()
    {
        SolarWeek w = SolarWeek.FromYm(2023, 10, 0, 0).Next(-1);
        Assert.Equal("第五周", w.GetName());
        Assert.Equal("2023年9月第五周", w.ToString());
    }

    [Fact]
    public void Test10()
    {
        SolarWeek w = SolarWeek.FromYm(2023, 10, 0, 0).Next(-5);
        Assert.Equal("第一周", w.GetName());
        Assert.Equal("2023年9月第一周", w.ToString());
    }

    [Fact]
    public void Test11()
    {
        SolarWeek w = SolarWeek.FromYm(2023, 10, 0, 0).Next(-6);
        Assert.Equal("第四周", w.GetName());
        Assert.Equal("2023年8月第四周", w.ToString());
    }

    [Fact]
    public void Test12()
    {
        SolarDay solar = SolarDay.FromYmd(1582, 10, 1);
        Assert.Equal(1, solar.Week.Index);
    }

    [Fact]
    public void Test13()
    {
        SolarDay solar = SolarDay.FromYmd(1582, 10, 15);
        Assert.Equal(5, solar.Week.Index);
    }

    [Fact]
    public void Test14()
    {
        SolarDay solar = SolarDay.FromYmd(1129, 11, 17);
        Assert.Equal(0, solar.Week.Index);
    }

    [Fact]
    public void Test15()
    {
        SolarDay solar = SolarDay.FromYmd(1129, 11, 1);
        Assert.Equal(5, solar.Week.Index);
    }

    [Fact]
    public void Test16()
    {
        SolarDay solar = SolarDay.FromYmd(8, 11, 1);
        Assert.Equal(4, solar.Week.Index);
    }

    [Fact]
    public void Test17()
    {
        SolarDay solar = SolarDay.FromYmd(1582, 9, 30);
        Assert.Equal(0, solar.Week.Index);
    }

    [Fact]
    public void Test18()
    {
        SolarDay solar = SolarDay.FromYmd(1582, 1, 1);
        Assert.Equal(1, solar.Week.Index);
    }

    [Fact]
    public void Test19()
    {
        SolarDay solar = SolarDay.FromYmd(1500, 2, 29);
        Assert.Equal(6, solar.Week.Index);
    }

    [Fact]
    public void Test20()
    {
        SolarDay solar = SolarDay.FromYmd(9865, 7, 26);
        Assert.Equal(3, solar.Week.Index);
    }

    [Fact]
    public void Test21()
    {
        LunarWeek week = LunarWeek.FromYm(2023, 1, 0, 2);
        Assert.Equal("农历癸卯年正月第一周", week.ToString());
        Assert.Equal("农历壬寅年十二月廿六", week.FirstDay.ToString());
    }

    [Fact]
    public void Test22()
    {
        SolarWeek week = SolarWeek.FromYm(2023, 1, 0, 2);
        Assert.Equal("2023年1月第一周", week.ToString());
        Assert.Equal("2022年12月27日", week.FirstDay.ToString());
    }

    [Fact]
    public void Test24()
    {
        int start = 0;
        SolarWeek week = SolarWeek.FromYm(2024, 2, 2, start);
        Assert.Equal("2024年2月第三周", week.ToString());
        Assert.Equal(6, week.IndexInYear);

        week = SolarDay.FromYmd(2024, 2, 11).GetSolarWeek(start);
        Assert.Equal("2024年2月第三周", week.ToString());

        week = SolarDay.FromYmd(2024, 2, 17).GetSolarWeek(start);
        Assert.Equal("2024年2月第三周", week.ToString());

        week = SolarDay.FromYmd(2024, 2, 10).GetSolarWeek(start);
        Assert.Equal("2024年2月第二周", week.ToString());

        week = SolarDay.FromYmd(2024, 2, 18).GetSolarWeek(start);
        Assert.Equal("2024年2月第四周", week.ToString());
    }
}