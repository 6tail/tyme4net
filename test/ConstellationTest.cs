using tyme.solar;

namespace test;

/// <summary>
/// 星座测试
/// </summary>
public class ConstellationTest
{
    [Fact]
    public void Test0()
    {
        Assert.Equal("白羊", SolarDay.FromYmd(2020,3,21).Constellation.GetName());
        Assert.Equal("白羊", SolarDay.FromYmd(2020,4,19).Constellation.GetName());
    }
    
    [Fact]
    public void Test1() {
        Assert.Equal("金牛", SolarDay.FromYmd(2020,4,20).Constellation.GetName());
        Assert.Equal("金牛", SolarDay.FromYmd(2020,5,20).Constellation.GetName());
    }

    [Fact]
    public void Test2() {
        Assert.Equal("双子", SolarDay.FromYmd(2020,5,21).Constellation.GetName());
        Assert.Equal("双子", SolarDay.FromYmd(2020,6,21).Constellation.GetName());
    }

    [Fact]
    public void Test3() {
        Assert.Equal("巨蟹", SolarDay.FromYmd(2020,6,22).Constellation.GetName());
        Assert.Equal("巨蟹", SolarDay.FromYmd(2020,7,22).Constellation.GetName());
    }
}