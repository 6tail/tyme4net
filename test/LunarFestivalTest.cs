using tyme.festival;
using tyme.lunar;

namespace test;

/// <summary>
/// 农历传统节日测试
/// </summary>
public class LunarFestivalTest
{
    [Fact]
    public void Test0()
    {
        for (int i = 0, j = LunarFestival.Names.Length; i < j; i++)
        {
            var f = LunarFestival.FromIndex(2023, i);
            Assert.NotNull(f);
            Assert.Equal(LunarFestival.Names[i], f.GetName());
        }
    }

    [Fact]
    public void Test1()
    {
        var f = LunarFestival.FromIndex(2023, 0);
        Assert.NotNull(f);
        for (int i = 0, j = LunarFestival.Names.Length; i < j; i++)
        {
            Assert.Equal(LunarFestival.Names[i], f.Next(i).GetName());
        }
    }

    [Fact]
    public void Test2()
    {
        var f = LunarFestival.FromIndex(2023, 0);
        Assert.NotNull(f);
        Assert.Equal("农历甲辰年正月初一 春节", f.Next(13).ToString());
        Assert.Equal("农历壬寅年十一月廿九 冬至节", f.Next(-3).ToString());
    }

    [Fact]
    public void Test3()
    {
        var f = LunarFestival.FromIndex(2023, 0);
        Assert.NotNull(f);
        Assert.Equal("农历壬寅年三月初五 清明节", f.Next(-9).ToString());
    }

    [Fact]
    public void Test4()
    {
        var f = LunarDay.FromYmd(2010, 1, 15).Festival;
        Assert.NotNull(f);
        Assert.Equal("农历庚寅年正月十五 元宵节", f.ToString());
    }

    [Fact]
    public void Test5()
    {
        var f = LunarDay.FromYmd(2021, 12, 29).Festival;
        Assert.NotNull(f);
        Assert.Equal("农历辛丑年十二月廿九 除夕", f.ToString());
    }
}