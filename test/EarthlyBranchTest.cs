using tyme.sixtycycle;

namespace test;

/// <summary>
/// 地支测试
/// </summary>
public class EarthlyBranchTest
{
    [Fact]
    public void Test0()
    {
        Assert.Equal("子", EarthBranch.FromIndex(0).GetName());
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal(0, EarthBranch.FromName("子").Index);
    }
}