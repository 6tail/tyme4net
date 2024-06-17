using tyme.festival;
using tyme.solar;

namespace test;

/// <summary>
/// 公历现代节日测试
/// </summary>
public class SolarFestivalTest
{
    [Fact]
    public void Test0()
    {
        for (int i = 0, j = SolarFestival.Names.Length; i < j; i++)
        {
            var f = SolarFestival.FromIndex(2023, i);
            Assert.NotNull(f);
            Assert.Equal(SolarFestival.Names[i], f.GetName());
        }
    }

    [Fact]
    public void Test1()
    {
        var f = SolarFestival.FromIndex(2023, 0);
        Assert.NotNull(f);
        for (int i = 0, j = SolarFestival.Names.Length; i < j; i++)
        {
            Assert.Equal(SolarFestival.Names[i], f.Next(i).GetName());
        }
    }

    [Fact]
    public void Test2()
    {
        var f = SolarFestival.FromIndex(2023, 0);
        Assert.NotNull(f);
        Assert.Equal("2024年5月1日 五一劳动节", f.Next(13).ToString());
        Assert.Equal("2022年8月1日 八一建军节", f.Next(-3).ToString());
    }

    [Fact]
    public void Test3()
    {
        var f = SolarFestival.FromIndex(2023, 0);
        Assert.NotNull(f);
        Assert.Equal("2022年3月8日 三八妇女节", f.Next(-9).ToString());
    }

    [Fact]
    public void Test4()
    {
        var f = SolarDay.FromYmd(2010, 1, 1).Festival;
        Assert.NotNull(f);
        Assert.Equal("2010年1月1日 元旦", f.ToString());
    }

    [Fact]
    public void Test5()
    {
        var f = SolarDay.FromYmd(2021, 5, 4).Festival;
        Assert.NotNull(f);
        Assert.Equal("2021年5月4日 五四青年节", f.ToString());
    }

    [Fact]
    public void Test6()
    {
        var f = SolarDay.FromYmd(1939, 5, 4).Festival;
        Assert.Null(f);
    }
}