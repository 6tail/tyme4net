using tyme.enums;
using tyme.solar;

namespace test;

/// <summary>
/// 人元司令分野测试
/// </summary>
public class HideHeavenStemDayTest
{
    [Fact]
    public void Test0()
    {
        var d = SolarDay.FromYmd(2024, 12, 4).HideHeavenStemDay;
        Assert.Equal("本气", d.HideHeavenStem.Type.GetName());
        Assert.Equal("壬", d.HideHeavenStem.GetName());
        Assert.Equal("壬", d.HideHeavenStem.ToString());
        Assert.Equal("水", d.HideHeavenStem.HeavenStem.Element.GetName());
        
        Assert.Equal("壬水", d.GetName());
        Assert.Equal(15, d.DayIndex);
        Assert.Equal("壬水第16天", d.ToString());
    }

    [Fact]
    public void Test1()
    {
        var d = SolarDay.FromYmd(2024, 11, 7).HideHeavenStemDay;
        Assert.Equal("余气", d.HideHeavenStem.Type.GetName());
        Assert.Equal("戊", d.HideHeavenStem.GetName());
        Assert.Equal("戊", d.HideHeavenStem.ToString());
        Assert.Equal("土", d.HideHeavenStem.HeavenStem.Element.GetName());
        
        Assert.Equal("戊土", d.GetName());
        Assert.Equal(0, d.DayIndex);
        Assert.Equal("戊土第1天", d.ToString());
    }
}