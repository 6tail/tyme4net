using tyme.culture;
using tyme.sixtycycle;

namespace test;

/// <summary>
/// 五行测试
/// </summary>
public class ElementTest
{
    /// <summary>
    /// 金克木
    /// </summary>
    [Fact]
    public void Test0()
    {
        Assert.Equal(Element.FromName("木"), Element.FromName("金").GetRestrain());
    }

    /// <summary>
    /// 火生土
    /// </summary>
    [Fact]
    public void Test1()
    {
        Assert.Equal(Element.FromName("土"), Element.FromName("火").GetReinforce());
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal("火", HeavenStem.FromName("丙").Element.GetName());
    }

    [Fact]
    public void Test3()
    {
        // 地支寅的五行为木
        Assert.Equal("木", EarthBranch.FromName("寅").Element.GetName());

        // 地支寅的五行(木)生火
        Assert.Equal(Element.FromName("火"), EarthBranch.FromName("寅").Element.GetReinforce());
    }

    /// <summary>
    /// 生我的：火生土
    /// </summary>
    [Fact]
    public void Test4()
    {
        Assert.Equal(Element.FromName("火"), Element.FromName("土").GetReinforced());
    }
}