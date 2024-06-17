using tyme.solar;

namespace test;

/// <summary>
/// 数九测试
/// </summary>
public class NineDayTest
{
    [Fact]
    public void Test0()
    {
        var d = SolarDay.FromYmd(2020, 12, 21).NineDay;
        Assert.Equal("一九", d.GetName());
        Assert.Equal("一九", d.Nine.ToString());
        Assert.Equal("一九第1天", d.ToString());
    }

    [Fact]
    public void Test1()
    {
        var d = SolarDay.FromYmd(2020, 12, 22).NineDay;
        Assert.Equal("一九", d.GetName());
        Assert.Equal("一九", d.Nine.ToString());
        Assert.Equal("一九第2天", d.ToString());
    }

    [Fact]
    public void Test2()
    {
        var d = SolarDay.FromYmd(2020, 1, 7).NineDay;
        Assert.Equal("二九", d.GetName());
        Assert.Equal("二九", d.Nine.ToString());
        Assert.Equal("二九第8天", d.ToString());
    }

    [Fact]
    public void Test3()
    {
        var d = SolarDay.FromYmd(2021, 1, 6).NineDay;
        Assert.Equal("二九", d.GetName());
        Assert.Equal("二九", d.Nine.ToString());
        Assert.Equal("二九第8天", d.ToString());
    }

    [Fact]
    public void Test4()
    {
        var d = SolarDay.FromYmd(2021, 1, 8).NineDay;
        Assert.Equal("三九", d.GetName());
        Assert.Equal("三九", d.Nine.ToString());
        Assert.Equal("三九第1天", d.ToString());
    }

    [Fact]
    public void Test5()
    {
        var d = SolarDay.FromYmd(2021, 3, 5).NineDay;
        Assert.Equal("九九", d.GetName());
        Assert.Equal("九九", d.Nine.ToString());
        Assert.Equal("九九第3天", d.ToString());
    }

    [Fact]
    public void Test6()
    {
        var d = SolarDay.FromYmd(2021, 7, 5).NineDay;
        Assert.Null(d);
    }
}