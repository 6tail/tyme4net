using tyme.culture.star.nine;
using tyme.lunar;
using tyme.solar;

namespace test;

/// <summary>
/// 九星测试
/// </summary>
public class NineStarTest
{
    [Fact]
    public void Test0()
    {
        NineStar nineStar = LunarYear.FromYear(1985).NineStar;
        Assert.Equal("六", nineStar.GetName());
        Assert.Equal("六白金", nineStar.ToString());
    }

    [Fact]
    public void Test1()
    {
        NineStar nineStar = LunarYear.FromYear(2022).NineStar;
        Assert.Equal("五黄土", nineStar.ToString());
        Assert.Equal("玉衡", nineStar.Dipper.ToString());
    }

    [Fact]
    public void Test2()
    {
        NineStar nineStar = LunarYear.FromYear(2033).NineStar;
        Assert.Equal("三碧木", nineStar.ToString());
        Assert.Equal("天玑", nineStar.Dipper.ToString());
    }

    [Fact]
    public void Test3()
    {
        NineStar nineStar = LunarMonth.FromYm(1985, 2).NineStar;
        Assert.Equal("四绿木", nineStar.ToString());
        Assert.Equal("天权", nineStar.Dipper.ToString());
    }

    [Fact]
    public void Test4()
    {
        NineStar nineStar = LunarMonth.FromYm(1985, 2).NineStar;
        Assert.Equal("四绿木", nineStar.ToString());
        Assert.Equal("天权", nineStar.Dipper.ToString());
    }

    [Fact]
    public void Test5()
    {
        NineStar nineStar = LunarMonth.FromYm(2022, 1).NineStar;
        Assert.Equal("二黒土", nineStar.ToString());
        Assert.Equal("天璇", nineStar.Dipper.ToString());
    }

    [Fact]
    public void Test6()
    {
        NineStar nineStar = LunarMonth.FromYm(2033, 1).NineStar;
        Assert.Equal("五黄土", nineStar.ToString());
        Assert.Equal("玉衡", nineStar.Dipper.ToString());
    }

    [Fact]
    public void Test7()
    {
        NineStar nineStar = SolarDay.FromYmd(1985, 2, 19).GetLunarDay().NineStar;
        Assert.Equal("五黄土", nineStar.ToString());
        Assert.Equal("玉衡", nineStar.Dipper.ToString());
    }

    [Fact]
    public void Test8()
    {
        NineStar nineStar = LunarDay.FromYmd(2022, 1, 1).NineStar;
        Assert.Equal("四绿木", nineStar.ToString());
        Assert.Equal("天权", nineStar.Dipper.ToString());
    }

    [Fact]
    public void Test9()
    {
        NineStar nineStar = LunarDay.FromYmd(2033, 1, 1).NineStar;
        Assert.Equal("一白水", nineStar.ToString());
        Assert.Equal("天枢", nineStar.Dipper.ToString());
    }

    [Fact]
    public void Test10()
    {
        NineStar nineStar = LunarHour.FromYmdHms(2033, 1, 1, 12, 0, 0).NineStar;
        Assert.Equal("七赤金", nineStar.ToString());
        Assert.Equal("摇光", nineStar.Dipper.ToString());
    }

    [Fact]
    public void Test11()
    {
        NineStar nineStar = LunarHour.FromYmdHms(2011, 5, 3, 23, 0, 0).NineStar;
        Assert.Equal("七赤金", nineStar.ToString());
        Assert.Equal("摇光", nineStar.Dipper.ToString());
    }
}