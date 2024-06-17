using tyme.sixtycycle;

namespace test;

/// <summary>
/// 六十甲子测试
/// </summary>
public class SixtyCycleTest
{
    [Fact]
    public void Test0()
    {
        Assert.Equal("丁丑", SixtyCycle.FromIndex(13).GetName());
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal(13, SixtyCycle.FromName("丁丑").Index);
    }

    /// <summary>
    /// 五行
    /// </summary>
    [Fact]
    public void Test2()
    {
        Assert.Equal("石榴木", SixtyCycle.FromName("辛酉").Sound.GetName());
        Assert.Equal("剑锋金", SixtyCycle.FromName("癸酉").Sound.GetName());
        Assert.Equal("平地木", SixtyCycle.FromName("己亥").Sound.GetName());
    }

    /// <summary>
    /// 旬
    /// </summary>
    [Fact]
    public void Test3()
    {
        Assert.Equal("甲子", SixtyCycle.FromName("甲子").Ten.GetName());
        Assert.Equal("甲寅", SixtyCycle.FromName("乙卯").Ten.GetName());
        Assert.Equal("甲申", SixtyCycle.FromName("癸巳").Ten.GetName());
    }

    /// <summary>
    /// 旬空
    /// </summary>
    [Fact]
    public void Test4()
    {
        Assert.Equal("戌亥", string.Join("", SixtyCycle.FromName("甲子").ExtraEarthBranches.Select(o => o.ToString())));
        Assert.Equal("子丑", string.Join("", SixtyCycle.FromName("乙卯").ExtraEarthBranches.Select(o => o.ToString())));
        Assert.Equal("午未", string.Join("", SixtyCycle.FromName("癸巳").ExtraEarthBranches.Select(o => o.ToString())));
    }

    /// <summary>
    /// 地势(长生十二神)
    /// </summary>
    [Fact]
    public void Test5()
    {
        Assert.Equal("长生", HeavenStem.FromName("丙").GetTerrain(EarthBranch.FromName("寅")).GetName());
        Assert.Equal("沐浴", HeavenStem.FromName("辛").GetTerrain(EarthBranch.FromName("亥")).GetName());
    }
}