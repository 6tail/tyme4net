using tyme.culture;
using tyme.rabbyung;
using tyme.solar;

namespace test;

/// <summary>
/// 藏历日测试
/// </summary>
public class RabByungDayTest
{
    [Fact]
    public void Test0()
    {
        Assert.Equal("第十六饶迥铁虎年十二月初一", SolarDay.FromYmd(1951, 1, 8).GetRabByungDay().ToString());
        Assert.Equal("1951年1月8日", RabByungDay.FromElementZodiac(15, RabByungElement.FromName("铁"), Zodiac.FromName("虎"), 12, 1).GetSolarDay().ToString());
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal("第十八饶迥铁马年十二月三十", SolarDay.FromYmd(2051, 2, 11).GetRabByungDay().ToString());
        Assert.Equal("2051年2月11日", RabByungDay.FromElementZodiac(17, RabByungElement.FromName("铁"), Zodiac.FromName("马"), 12, 30).GetSolarDay().ToString());
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal("第十七饶迥木蛇年二月廿五", SolarDay.FromYmd(2025, 4, 23).GetRabByungDay().ToString());
        Assert.Equal("2025年4月23日", RabByungDay.FromElementZodiac(16, RabByungElement.FromName("木"), Zodiac.FromName("蛇"), 2, 25).GetSolarDay().ToString());
    }

    [Fact]
    public void Test3()
    {
        Assert.Equal("第十六饶迥铁兔年正月初二", SolarDay.FromYmd(1951, 2, 8).GetRabByungDay().ToString());
        Assert.Equal("1951年2月8日", RabByungDay.FromElementZodiac(15, RabByungElement.FromName("铁"), Zodiac.FromName("兔"), 1, 2).GetSolarDay().ToString());
    }

    [Fact]
    public void Test4()
    {
        Assert.Equal("第十六饶迥铁虎年十二月闰十六", SolarDay.FromYmd(1951, 1, 24).GetRabByungDay().ToString());
        Assert.Equal("1951年1月24日", RabByungDay.FromElementZodiac(15, RabByungElement.FromName("铁"), Zodiac.FromName("虎"), 12, -16).GetSolarDay().ToString());
    }

    [Fact]
    public void Test5()
    {
        Assert.Equal("第十六饶迥铁牛年五月十一", SolarDay.FromYmd(1961, 6, 24).GetRabByungDay().ToString());
        Assert.Equal("1961年6月24日", RabByungDay.FromElementZodiac(15, RabByungElement.FromName("铁"), Zodiac.FromName("牛"), 5, 11).GetSolarDay().ToString());
    }

    [Fact]
    public void Test6()
    {
        Assert.Equal("第十六饶迥铁兔年十二月廿八", SolarDay.FromYmd(1952, 2, 23).GetRabByungDay().ToString());
        Assert.Equal("1952年2月23日", RabByungDay.FromElementZodiac(15, RabByungElement.FromName("铁"), Zodiac.FromName("兔"), 12, 28).GetSolarDay().ToString());
    }

    [Fact]
    public void Test7()
    {
        Assert.Equal("第十七饶迥木蛇年二月廿九", SolarDay.FromYmd(2025, 4, 26).GetRabByungDay().ToString());
    }

    [Fact]
    public void Test8()
    {
        Assert.Equal("第十七饶迥木蛇年二月廿七", SolarDay.FromYmd(2025, 4, 25).GetRabByungDay().ToString());
    }
}