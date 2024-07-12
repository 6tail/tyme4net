using tyme.solar;

namespace test;

/// <summary>
/// 神煞测试
/// </summary>
public class GodTest
{
    [Fact]
    public void Test0()
    {
        var lunar = SolarDay.FromYmd(2004, 2, 16).GetLunarDay();
        var gods = lunar.Gods;
        var ji = (from god in gods where "吉" == god.Luck.GetName() select god.GetName()).ToList();
        var xiong = (from god in gods where "凶" == god.Luck.GetName() select god.GetName()).ToList();

        Assert.Equal(new List<string>
        {
            "天恩", "续世", "明堂"
        }, ji);
        Assert.Equal(new List<string>
        {
            "月煞", "月虚", "血支", "天贼", "五虚", "土符", "归忌", "血忌"
        }, xiong);
    }
}