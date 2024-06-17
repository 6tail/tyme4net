using tyme.solar;

namespace test;

/// <summary>
/// 建除十二值神测试
/// </summary>
public class DutyTest
{
    [Fact]
    public void Test0() {
        Assert.Equal("闭", SolarDay.FromYmd(2023, 10, 30).GetLunarDay().Duty.GetName());
    }

    [Fact]
    public void Test1() {
        Assert.Equal("建", SolarDay.FromYmd(2023, 10, 19).GetLunarDay().Duty.GetName());
    }

    [Fact]
    public void Test2() {
        Assert.Equal("除", SolarDay.FromYmd(2023, 10, 7).GetLunarDay().Duty.GetName());
    }

    [Fact]
    public void Test3() {
        Assert.Equal("除", SolarDay.FromYmd(2023, 10, 8).GetLunarDay().Duty.GetName());
    }

}