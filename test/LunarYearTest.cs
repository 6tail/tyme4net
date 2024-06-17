using tyme.lunar;
using Xunit.Abstractions;

namespace test;

/// <summary>
/// 农历年测试
/// </summary>
public class LunarYearTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public LunarYearTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test0()
    {
        Assert.Equal("农历癸卯年", LunarYear.FromYear(2023).GetName());
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal("农历戊申年", LunarYear.FromYear(2023).Next(5).GetName());
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal("农历戊戌年", LunarYear.FromYear(2023).Next(-5).GetName());
    }

    /// <summary>
    /// 农历年的干支
    /// </summary>
    [Fact]
    public void Test3()
    {
        Assert.Equal("庚子", LunarYear.FromYear(2020).SixtyCycle.GetName());
    }

    /// <summary>
    /// 农历年的生肖(农历年.干支.地支.生肖)
    /// </summary>
    [Fact]
    public void Test4()
    {
        Assert.Equal("虎", LunarYear.FromYear(1986).SixtyCycle.EarthBranch.GetZodiac().GetName());
    }

    [Fact]
    public void Test5()
    {
        Assert.Equal(12, LunarYear.FromYear(151).LeapMonth);
    }

    [Fact]
    public void Test6()
    {
        Assert.Equal(1, LunarYear.FromYear(2357).LeapMonth);
    }

    [Fact]
    public void Test7()
    {
        var y = LunarYear.FromYear(2023);
        Assert.Equal("癸卯", y.SixtyCycle.GetName());
        Assert.Equal("兔", y.SixtyCycle.EarthBranch.GetZodiac().GetName());
    }

    [Fact]
    public void Test8()
    {
        Assert.Equal("上元", LunarYear.FromYear(1864).Twenty.Sixty.GetName());
    }

    [Fact]
    public void Test9()
    {
        Assert.Equal("上元", LunarYear.FromYear(1923).Twenty.Sixty.GetName());
    }

    [Fact]
    public void Test10()
    {
        Assert.Equal("中元", LunarYear.FromYear(1924).Twenty.Sixty.GetName());
    }

    [Fact]
    public void Test11()
    {
        Assert.Equal("中元", LunarYear.FromYear(1983).Twenty.Sixty.GetName());
    }

    [Fact]
    public void Test12()
    {
        Assert.Equal("下元", LunarYear.FromYear(1984).Twenty.Sixty.GetName());
    }

    [Fact]
    public void Test13()
    {
        Assert.Equal("下元", LunarYear.FromYear(2043).Twenty.Sixty.GetName());
    }

    [Fact]
    public void Test14()
    {
        Assert.Equal("一运", LunarYear.FromYear(1864).Twenty.GetName());
    }

    [Fact]
    public void Test15()
    {
        Assert.Equal("一运", LunarYear.FromYear(1883).Twenty.GetName());
    }

    [Fact]
    public void Test16()
    {
        Assert.Equal("二运", LunarYear.FromYear(1884).Twenty.GetName());
    }

    [Fact]
    public void Test17()
    {
        Assert.Equal("二运", LunarYear.FromYear(1903).Twenty.GetName());
    }

    [Fact]
    public void Test18()
    {
        Assert.Equal("三运", LunarYear.FromYear(1904).Twenty.GetName());
    }

    [Fact]
    public void Test19()
    {
        Assert.Equal("三运", LunarYear.FromYear(1923).Twenty.GetName());
    }

    [Fact]
    public void Test20()
    {
        Assert.Equal("八运", LunarYear.FromYear(2004).Twenty.GetName());
    }

    [Fact]
    public void Test21()
    {
        var year = LunarYear.FromYear(1);
        Assert.Equal("六运", year.Twenty.GetName());
        Assert.Equal("中元", year.Twenty.Sixty.GetName());
    }

    [Fact]
    public void Test22()
    {
        var year = LunarYear.FromYear(1863);
        Assert.Equal("九运", year.Twenty.GetName());
        Assert.Equal("下元", year.Twenty.Sixty.GetName());
    }

    /// <summary>
    /// 生成农历年历示例
    /// </summary>
    [Fact]
    public void Test23()
    {
        var year = LunarYear.FromYear(2023);
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