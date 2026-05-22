using tyme.hijri;
using tyme.solar;

namespace test;

/// <summary>
/// 回历日测试
/// </summary>
public class HijriDayTest
{
    [Fact]
    public void Test0() {
        Assert.Equal("1年穆哈兰姆月1日", SolarDay.FromYmd(622, 7, 16).GetHijriDay().ToString());
    }

    [Fact]
    public void Test1() {
        Assert.Equal("1447年都尔喀尔德月26日", SolarDay.FromYmd(2026, 5, 13).GetHijriDay().ToString());
        Assert.Equal("2026年5月13日", HijriDay.FromYmd(1447, 11, 26).GetSolarDay().ToString());
    }

    [Fact]
    public void Test2() {
        Assert.Equal("-538年都尔黑哲月12日", SolarDay.FromYmd(100, 7, 8).GetHijriDay().ToString());
        Assert.Equal("100年7月8日", HijriDay.FromYmd(-538, 12, 12).GetSolarDay().ToString());
    }

    [Fact]
    public void Test3() {
        Assert.Equal("0年都尔黑哲月29日", SolarDay.FromYmd(622, 7, 15).GetHijriDay().ToString());
        Assert.Equal("622年7月15日", HijriDay.FromYmd(0, 12, 29).GetSolarDay().ToString());
    }

    [Fact]
    public void Test4() {
        Assert.Equal("-640年主马达·敖外鲁月16日", SolarDay.FromYmd(1, 1, 1).GetHijriDay().ToString());
        Assert.Equal("1年1月1日", HijriDay.FromYmd(-640, 5, 16).GetSolarDay().ToString());
    }

    [Fact]
    public void Test5() {
        Assert.Equal("9666年赖比尔·阿色尼月2日", SolarDay.FromYmd(9999, 12, 31).GetHijriDay().ToString());
        Assert.Equal("9999年12月31日", HijriDay.FromYmd(9666, 4, 2).GetSolarDay().ToString());
    }
}