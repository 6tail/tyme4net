using tyme.solar;

namespace test;

/// <summary>
/// 儒略日测试
/// </summary>
public class JulianDayTest
{
    [Fact]
    public void Test0()
    {
        Assert.Equal("2023年1月1日", SolarDay.FromYmd(2023, 1, 1).GetJulianDay().GetSolarDay().ToString());
    }
}