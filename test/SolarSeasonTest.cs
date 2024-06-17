using tyme.solar;

namespace test;

/// <summary>
/// 公历季度测试
/// </summary>
public class SolarSeasonTest
{
    [Fact]
    public void Test0()
    {
        var season = SolarSeason.FromIndex(2023, 0);
        Assert.Equal("2023年一季度", season.ToString());
        Assert.Equal("2021年四季度", season.Next(-5).ToString());
    }
}