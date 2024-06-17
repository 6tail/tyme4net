using tyme.solar;

namespace test;

/// <summary>
/// 节气测试
/// </summary>
public class SolarTermTest
{
    [Fact]
    public void Test0()
    {
        // 冬至在去年，2022-12-22 05:48:11
        SolarTerm dongZhi = SolarTerm.FromName(2023, "冬至");
        Assert.Equal("冬至", dongZhi.GetName());
        Assert.Equal(0, dongZhi.Index);
        // 公历日
        Assert.Equal("2022年12月22日", dongZhi.JulianDay.GetSolarDay().ToString());

        // 冬至顺推23次，就是大雪 2023-12-07 17:32:55
        SolarTerm daXue = dongZhi.Next(23);
        Assert.Equal("大雪", daXue.GetName());
        Assert.Equal(23, daXue.Index);
        Assert.Equal("2023年12月7日", daXue.JulianDay.GetSolarDay().ToString());

        // 冬至逆推2次，就是上一年的小雪 2022-11-22 16:20:28
        SolarTerm xiaoXue = dongZhi.Next(-2);
        Assert.Equal("小雪", xiaoXue.GetName());
        Assert.Equal(22, xiaoXue.Index);
        Assert.Equal("2022年11月22日", xiaoXue.JulianDay.GetSolarDay().ToString());

        // 冬至顺推24次，就是下一个冬至 2023-12-22 11:27:20
        SolarTerm dongZhi2 = dongZhi.Next(24);
        Assert.Equal("冬至", dongZhi2.GetName());
        Assert.Equal(0, dongZhi2.Index);
        Assert.Equal("2023年12月22日", dongZhi2.JulianDay.GetSolarDay().ToString());
    }

    [Fact]
    public void Test1()
    {
        // 公历2023年的雨水，2023-02-19 06:34:16
        var jq = SolarTerm.FromName(2023, "雨水");
        Assert.Equal("雨水", jq.GetName());
        Assert.Equal(4, jq.Index);
    }

    [Fact]
    public void Test2()
    {
        // 公历2023年的大雪，2023-12-07 17:32:55
        var jq = SolarTerm.FromName(2023, "大雪");
        Assert.Equal("大雪", jq.GetName());
        // 索引
        Assert.Equal(23, jq.Index);
        // 公历
        Assert.Equal("2023年12月7日", jq.JulianDay.GetSolarDay().ToString());
        // 农历
        Assert.Equal("农历癸卯年十月廿五", jq.JulianDay.GetSolarDay().GetLunarDay().ToString());
        // 推移
        Assert.Equal("雨水", jq.Next(5).GetName());
    }

    [Fact]
    public void Test3()
    {
        Assert.Equal("寒露", SolarDay.FromYmd(2023, 10, 10).Term.GetName());
    }

    [Fact]
    public void Test4()
    {
        // 大雪当天
        Assert.Equal("大雪第1天", SolarDay.FromYmd(2023, 12, 7).TermDay.ToString());
        // 天数索引
        Assert.Equal(0, SolarDay.FromYmd(2023, 12, 7).TermDay.DayIndex);

        Assert.Equal("大雪第2天", SolarDay.FromYmd(2023, 12, 8).TermDay.ToString());
        Assert.Equal("大雪第15天", SolarDay.FromYmd(2023, 12, 21).TermDay.ToString());

        Assert.Equal("冬至第1天", SolarDay.FromYmd(2023, 12, 22).TermDay.ToString());
    }
}