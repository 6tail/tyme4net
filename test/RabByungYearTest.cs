using tyme.culture;
using tyme.rabbyung;

namespace test;

/// <summary>
/// 藏历年测试
/// </summary>
public class RabByungYearTest
{
    [Fact]
    public void Test0()
    {
        var y = RabByungYear.FromElementZodiac(0, RabByungElement.FromName("火"), Zodiac.FromName("兔"));
        Assert.Equal("第一饶迥火兔年", y.GetName());
        Assert.Equal("1027年", y.GetSolarYear().GetName());
        Assert.Equal("丁卯", y.SixtyCycle.GetName());
        Assert.Equal(10, y.LeapMonth);
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal("第一饶迥火兔年", RabByungYear.FromYear(1027).GetName());
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal("第十七饶迥铁虎年", RabByungYear.FromYear(2010).GetName());
    }

    [Fact]
    public void Test3()
    {
        Assert.Equal(5, RabByungYear.FromYear(2043).LeapMonth);
        Assert.Equal(0, RabByungYear.FromYear(2044).LeapMonth);
    }

    [Fact]
    public void Test4()
    {
        Assert.Equal("第十六饶迥铁牛年", RabByungYear.FromYear(1961).GetName());
    }
}