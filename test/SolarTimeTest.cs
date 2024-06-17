using tyme.solar;

namespace test;

/// <summary>
/// 公历时刻测试
/// </summary>
public class SolarTimeTest
{
    [Fact]
    public void Test0()
    {
        var time = SolarTime.FromYmdHms(2023, 1, 1, 13, 5, 20);
        Assert.Equal("13:05:20", time.GetName());
        Assert.Equal("13:04:59", time.Next(-21).GetName());
    }

    [Fact]
    public void Test1()
    {
        var time = SolarTime.FromYmdHms(2023, 1, 1, 13, 5, 20);
        Assert.Equal("13:05:20", time.GetName());
        Assert.Equal("14:06:01", time.Next(3641).GetName());
    }
}