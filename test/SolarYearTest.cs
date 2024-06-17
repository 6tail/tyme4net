using tyme.solar;
using Xunit.Abstractions;

namespace test;

/// <summary>
/// 公历年测试
/// </summary>
public class SolarYearTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public SolarYearTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test0()
    {
        Assert.Equal("2023年", SolarYear.FromYear(2023).GetName());
    }

    [Fact]
    public void Test1()
    {
        Assert.False(SolarYear.FromYear(2023).IsLeap);
    }

    [Fact]
    public void Test2()
    {
        Assert.True(SolarYear.FromYear(1500).IsLeap);
    }

    [Fact]
    public void Test3()
    {
        Assert.False(SolarYear.FromYear(1700).IsLeap);
    }

    [Fact]
    public void Test4()
    {
        Assert.Equal(365, SolarYear.FromYear(2023).DayCount);
    }

    [Fact]
    public void Test5()
    {
        Assert.Equal("2028年", SolarYear.FromYear(2023).Next(5).GetName());
    }

    [Fact]
    public void Test6()
    {
        Assert.Equal("2018年", SolarYear.FromYear(2023).Next(-5).GetName());
    }

    /// <summary>
    /// 生成公历年历示例
    /// </summary>
    [Fact]
    public void Test7()
    {
        var year = SolarYear.FromYear(2024);
        foreach (var month in year.Months)
        {
            _testOutputHelper.WriteLine(month.ToString());
            foreach (var week in month.GetWeeks(1))
            {
                var s = week.GetName();
                s = week.Days.Aggregate(s, (current, day) => current + (" " + day.GetName()));
                _testOutputHelper.WriteLine(s);
            }

            _testOutputHelper.WriteLine("");
        }
    }
}