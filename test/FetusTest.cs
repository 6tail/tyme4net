using tyme.solar;

namespace test;

/// <summary>
/// 胎神测试
/// </summary>
public class FetusTest
{
    /// <summary>
    /// 逐日胎神
    /// </summary>
    [Fact]
    public void Test1()
    {
        Assert.Equal("碓磨厕 外东南", SolarDay.FromYmd(2021, 11, 13).GetLunarDay().FetusDay.GetName());
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal("占门碓 外东南", SolarDay.FromYmd(2021, 11, 12).GetLunarDay().FetusDay.GetName());
    }

    [Fact]
    public void Test3()
    {
        Assert.Equal("厨灶厕 外西南", SolarDay.FromYmd(2011, 11, 12).GetLunarDay().FetusDay.GetName());
    }
}