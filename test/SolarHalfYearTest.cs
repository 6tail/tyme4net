using tyme.solar;

namespace test;

/// <summary>
/// 公历半年测试
/// </summary>
public class SolarHalfYearTest
{
    [Fact]
    public void Test0()
    {
        Assert.Equal("上半年", SolarHalfYear.FromIndex(2023, 0).GetName());
        Assert.Equal("2023年上半年", SolarHalfYear.FromIndex(2023, 0).ToString());
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal("下半年", SolarHalfYear.FromIndex(2023, 1).GetName());
        Assert.Equal("2023年下半年", SolarHalfYear.FromIndex(2023, 1).ToString());
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal("下半年", SolarHalfYear.FromIndex(2023, 0).Next(1).GetName());
        Assert.Equal("2023年下半年", SolarHalfYear.FromIndex(2023, 0).Next(1).ToString());
    }

    [Fact]
    public void Test3()
    {
        Assert.Equal("上半年", SolarHalfYear.FromIndex(2023, 0).Next(2).GetName());
        Assert.Equal("2024年上半年", SolarHalfYear.FromIndex(2023, 0).Next(2).ToString());
    }

    [Fact]
    public void Test4()
    {
        Assert.Equal("上半年", SolarHalfYear.FromIndex(2023, 0).Next(-2).GetName());
        Assert.Equal("2022年上半年", SolarHalfYear.FromIndex(2023, 0).Next(-2).ToString());
    }

    [Fact]
    public void Test5()
    {
        Assert.Equal("2021年上半年", SolarHalfYear.FromIndex(2023, 0).Next(-4).ToString());
        Assert.Equal("2021年下半年", SolarHalfYear.FromIndex(2023, 0).Next(-3).ToString());
    }
}