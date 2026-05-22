using tyme.hijri;

namespace test;

/// <summary>
/// 回历年测试
/// </summary>
public class HijriYearTest
{
    [Fact]
    public void Test0()
    {
        Assert.False(HijriYear.FromYear(1).IsLeap);
        Assert.True(HijriYear.FromYear(2).IsLeap);
        Assert.False(HijriYear.FromYear(0).IsLeap);
        Assert.True(HijriYear.FromYear(-1).IsLeap);
    }
}