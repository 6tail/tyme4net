using tyme.solar;

namespace test;

/// <summary>
/// 三柱测试
/// </summary>
public class ThreePillarsTest
{
    [Fact]
    public void Test0()
    {
        Assert.Equal("甲戌 甲戌 甲戌", SolarDay.FromYmd(1034, 10, 2).GetSixtyCycleDay().ThreePillars.GetName());
    }
}