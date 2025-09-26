using tyme.culture.phenology;
using tyme.solar;

namespace test;

/// <summary>
/// 物候测试
/// </summary>
public class PhenologyTest
{
    [Fact]
    public void Test0()
    {
        var solarDay = SolarDay.FromYmd(2020, 4, 23);
        // 七十二候
        var phenology = solarDay.PhenologyDay;
        // 三候
        var threePhenology = phenology.Phenology.ThreePhenology;
        Assert.Equal("谷雨", solarDay.Term.GetName());
        Assert.Equal("初候", threePhenology.GetName());
        Assert.Equal("萍始生", phenology.GetName());
        // 该候的第5天
        Assert.Equal(4, phenology.DayIndex);
    }

    [Fact]
    public void Test1()
    {
        var solarDay = SolarDay.FromYmd(2021, 12, 26);
        // 七十二候
        var phenology = solarDay.PhenologyDay;
        // 三候
        var threePhenology = phenology.Phenology.ThreePhenology;
        Assert.Equal("冬至", solarDay.Term.GetName());
        Assert.Equal("二候", threePhenology.GetName());
        Assert.Equal("麋角解", phenology.GetName());
        // 该候的第1天
        Assert.Equal(0, phenology.DayIndex);
    }
    
    [Fact]
    public void Test2()
    {
        var p = Phenology.FromIndex(2026, 1);
        var jd = p.JulianDay;
        Assert.Equal("麋角解", p.GetName());
        Assert.Equal("2025年12月26日", jd.GetSolarDay().ToString());
        Assert.Equal("2025年12月26日 20:49:56", jd.GetSolarTime().ToString());
    }
    
    [Fact]
    public void Test3()
    {
        var p = SolarDay.FromYmd(2025, 12, 26).Phenology;
        var jd = p.JulianDay;
        Assert.Equal("麋角解", p.GetName());
        Assert.Equal("2025年12月26日", jd.GetSolarDay().ToString());
        Assert.Equal("2025年12月26日 20:49:56", jd.GetSolarTime().ToString());
    }
    
    [Fact]
    public void Test4()
    {
        Assert.Equal("蚯蚓结", SolarTime.FromYmdHms(2025, 12, 26, 20, 49, 38).Phenology.GetName());
        Assert.Equal("麋角解", SolarTime.FromYmdHms(2025, 12, 26, 20, 49, 56).Phenology.GetName());
    }
}