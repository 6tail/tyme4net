using tyme.solar;

namespace test;

/// <summary>
/// 方位测试
/// </summary>
public class DirectionTest
{
    /// <summary>
    /// 福神方位
    /// </summary>
    [Fact]
    public void Test1() {
        Assert.Equal("东南", SolarDay.FromYmd(2021, 11, 13).GetLunarDay().SixtyCycle.HeavenStem.MascotDirection.GetName());
    }

    /// <summary>
    /// 福神方位
    /// </summary>
    [Fact]
    public void Test2() {
        Assert.Equal("东南", SolarDay.FromYmd(2024, 1, 1).GetLunarDay().SixtyCycle.HeavenStem.MascotDirection.GetName());
    }

    /// <summary>
    /// 太岁方位
    /// </summary>
    [Fact]
    public void Test3() {
        Assert.Equal("东", SolarDay.FromYmd(2023, 11, 6).GetLunarDay().JupiterDirection.GetName());
    }
}