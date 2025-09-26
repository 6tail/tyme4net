using tyme.culture;
using tyme.lunar;
using tyme.solar;

namespace test;

/// <summary>
/// 月相测试
/// </summary>
public class PhaseTest
{
    [Fact]
    public void Test0()
    {
        var phase = Phase.FromName(2025, 7, "下弦月");
        Assert.Equal("2025年9月14日 18:32:57", phase.SolarTime.ToString());
    }

    [Fact]
    public void Test1()
    {
        var phase = Phase.FromIndex(2025, 7, 6);
        Assert.Equal("2025年9月14日 18:32:57", phase.SolarTime.ToString());
    }

    [Fact]
    public void Test2()
    {
        var phase = Phase.FromIndex(2025, 7, 8);
        Assert.Equal("2025年9月22日 03:54:07", phase.SolarTime.ToString());
    }

    [Fact]
    public void Test3()
    {
        var phase = SolarDay.FromYmd(2025, 9, 21).Phase;
        Assert.Equal("残月", phase.ToString());
    }

    [Fact]
    public void Test4()
    {
        var phase = LunarDay.FromYmd(2025, 7, 30).Phase;
        Assert.Equal("残月", phase.ToString());
    }

    [Fact]
    public void Test5()
    {
        var phase = SolarTime.FromYmdHms(2025, 9, 22, 4, 0, 0).Phase;
        Assert.Equal("蛾眉月", phase.ToString());
    }

    [Fact]
    public void Test6()
    {
        var phase = SolarTime.FromYmdHms(2025, 9, 22, 3, 0, 0).Phase;
        Assert.Equal("残月", phase.ToString());
    }

    [Fact]
    public void Test7()
    {
        var d = SolarDay.FromYmd(2023, 9, 15).PhaseDay;
        Assert.Equal("新月第1天", d.ToString());
    }

    [Fact]
    public void Test8()
    {
        var d = SolarDay.FromYmd(2023, 9, 17).PhaseDay;
        Assert.Equal("蛾眉月第2天", d.ToString());
    }

    [Fact]
    public void Test9()
    {
        var phase = SolarTime.FromYmdHms(2025, 9, 22, 3, 54, 7).Phase;
        Assert.Equal("新月", phase.ToString());
    }

    [Fact]
    public void Test10()
    {
        var phase = SolarTime.FromYmdHms(2025, 9, 22, 3, 54, 6).Phase;
        Assert.Equal("残月", phase.ToString());
    }

    [Fact]
    public void Test11()
    {
        var phase = SolarTime.FromYmdHms(2025, 9, 22, 3, 54, 8).Phase;
        Assert.Equal("蛾眉月", phase.ToString());
    }
}