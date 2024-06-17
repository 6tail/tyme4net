using tyme.sixtycycle;

namespace test;

/// <summary>
/// 天干测试
/// </summary>
public class HeavenlyStemTest
{
    [Fact]
    public void Test0()
    {
        Assert.Equal("甲", HeavenStem.FromIndex(0).GetName());
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal(0, HeavenStem.FromName("甲").Index);
    }

    /// <summary>
    /// 天干的五行生克
    /// </summary>
    [Fact]
    public void Test2()
    {
        Assert.Equal(HeavenStem.FromName("丙").Element, HeavenStem.FromName("甲").Element.GetReinforce());
    }

    /// <summary>
    /// 十神
    /// </summary>
    [Fact]
    public void Test3()
    {
        Assert.Equal("比肩", HeavenStem.FromName("甲").GetTenStar(HeavenStem.FromName("甲")).GetName());
        Assert.Equal("劫财", HeavenStem.FromName("甲").GetTenStar(HeavenStem.FromName("乙")).GetName());
        Assert.Equal("食神", HeavenStem.FromName("甲").GetTenStar(HeavenStem.FromName("丙")).GetName());
        Assert.Equal("伤官", HeavenStem.FromName("甲").GetTenStar(HeavenStem.FromName("丁")).GetName());
        Assert.Equal("偏财", HeavenStem.FromName("甲").GetTenStar(HeavenStem.FromName("戊")).GetName());
        Assert.Equal("正财", HeavenStem.FromName("甲").GetTenStar(HeavenStem.FromName("己")).GetName());
        Assert.Equal("七杀", HeavenStem.FromName("甲").GetTenStar(HeavenStem.FromName("庚")).GetName());
        Assert.Equal("正官", HeavenStem.FromName("甲").GetTenStar(HeavenStem.FromName("辛")).GetName());
        Assert.Equal("偏印", HeavenStem.FromName("甲").GetTenStar(HeavenStem.FromName("壬")).GetName());
        Assert.Equal("正印", HeavenStem.FromName("甲").GetTenStar(HeavenStem.FromName("癸")).GetName());
    }
}