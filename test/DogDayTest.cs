using tyme.solar;

namespace test;

/// <summary>
/// 三伏测试
/// </summary>
public class DogDayTest
{
    [Fact]
    public void Test0()
    {
        var d = SolarDay.FromYmd(2011, 7, 14).DogDay;
        Assert.Equal("初伏", d.GetName());
        Assert.Equal("初伏", d.Dog.ToString());
        Assert.Equal("初伏第1天", d.ToString());
    }

    [Fact]
    public void Test1()
    {
        var d = SolarDay.FromYmd(2011, 7, 23).DogDay;
        Assert.Equal("初伏", d.GetName());
        Assert.Equal("初伏", d.Dog.ToString());
        Assert.Equal("初伏第10天", d.ToString());
    }

    [Fact]
    public void Test2()
    {
        var d = SolarDay.FromYmd(2011, 7, 24).DogDay;
        Assert.Equal("中伏", d.GetName());
        Assert.Equal("中伏", d.Dog.ToString());
        Assert.Equal("中伏第1天", d.ToString());
    }

    [Fact]
    public void Test3()
    {
        var d = SolarDay.FromYmd(2011, 8, 12).DogDay;
        Assert.Equal("中伏", d.GetName());
        Assert.Equal("中伏", d.Dog.ToString());
        Assert.Equal("中伏第20天", d.ToString());
    }

    [Fact]
    public void Test4()
    {
        var d = SolarDay.FromYmd(2011, 8, 13).DogDay;
        Assert.Equal("末伏", d.GetName());
        Assert.Equal("末伏", d.Dog.ToString());
        Assert.Equal("末伏第1天", d.ToString());
    }

    [Fact]
    public void Test5()
    {
        var d = SolarDay.FromYmd(2011, 8, 22).DogDay;
        Assert.Equal("末伏", d.GetName());
        Assert.Equal("末伏", d.Dog.ToString());
        Assert.Equal("末伏第10天", d.ToString());
    }

    [Fact]
    public void Test6()
    {
        Assert.Null(SolarDay.FromYmd(2011, 7, 13).DogDay);
    }

    [Fact]
    public void Test7()
    {
        Assert.Null(SolarDay.FromYmd(2011, 8, 23).DogDay);
    }

    [Fact]
    public void Test8()
    {
        var d = SolarDay.FromYmd(2012, 7, 18).DogDay;
        Assert.Equal("初伏", d.GetName());
        Assert.Equal("初伏", d.Dog.ToString());
        Assert.Equal("初伏第1天", d.ToString());
    }

    [Fact]
    public void Test9()
    {
        var d = SolarDay.FromYmd(2012, 8, 5).DogDay;
        Assert.Equal("中伏", d.GetName());
        Assert.Equal("中伏", d.Dog.ToString());
        Assert.Equal("中伏第9天", d.ToString());
    }

    [Fact]
    public void Test10()
    {
        var d = SolarDay.FromYmd(2012, 8, 8).DogDay;
        Assert.Equal("末伏", d.GetName());
        Assert.Equal("末伏", d.Dog.ToString());
        Assert.Equal("末伏第2天", d.ToString());
    }
}