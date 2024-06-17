using tyme.solar;

namespace test;

/// <summary>
/// 公历月测试
/// </summary>
public class SolarMonthTest
{
    [Fact]
    public void Test0()
    {
        var m = SolarMonth.FromYm(2019, 5);
        Assert.Equal("5月", m.GetName());
        Assert.Equal("2019年5月", m.ToString());
    }

    [Fact]
    public void Test1()
    {
        var m = SolarMonth.FromYm(2023, 1);
        Assert.Equal(5, m.GetWeekCount(0));
        Assert.Equal(6, m.GetWeekCount(1));
        Assert.Equal(6, m.GetWeekCount(2));
        Assert.Equal(5, m.GetWeekCount(3));
        Assert.Equal(5, m.GetWeekCount(4));
        Assert.Equal(5, m.GetWeekCount(5));
        Assert.Equal(5, m.GetWeekCount(6));
    }

    [Fact]
    public void Test2()
    {
        var m = SolarMonth.FromYm(2023, 2);
        Assert.Equal(5, m.GetWeekCount(0));
        Assert.Equal(5, m.GetWeekCount(1));
        Assert.Equal(5, m.GetWeekCount(2));
        Assert.Equal(4, m.GetWeekCount(3));
        Assert.Equal(5, m.GetWeekCount(4));
        Assert.Equal(5, m.GetWeekCount(5));
        Assert.Equal(5, m.GetWeekCount(6));
    }

    [Fact]
    public void Test3()
    {
        var m = SolarMonth.FromYm(2023, 10).Next(1);
        Assert.Equal("11月", m.GetName());
        Assert.Equal("2023年11月", m.ToString());
    }

    [Fact]
    public void Test4()
    {
        var m = SolarMonth.FromYm(2023, 10);
        Assert.Equal("2023年12月", m.Next(2).ToString());
        Assert.Equal("2024年1月", m.Next(3).ToString());
        Assert.Equal("2023年5月", m.Next(-5).ToString());
        Assert.Equal("2023年1月", m.Next(-9).ToString());
        Assert.Equal("2022年12月", m.Next(-10).ToString());
        Assert.Equal("2025年10月", m.Next(24).ToString());
        Assert.Equal("2021年10月", m.Next(-24).ToString());
    }
}