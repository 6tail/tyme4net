using tyme.eightchar;
using tyme.eightchar.provider.impl;
using tyme.enums;
using tyme.lunar;
using tyme.sixtycycle;
using tyme.solar;
using Xunit.Abstractions;

namespace test;

/// <summary>
/// 八字测试
/// </summary>
public class EightCharTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public EightCharTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    /// <summary>
    /// 十神
    /// </summary>
    [Fact]
    public void Test1()
    {
        // 八字
        var eightChar = new EightChar("丙寅", "癸巳", "癸酉", "己未");

        // 年柱
        var year = eightChar.Year;
        // 月柱
        var month = eightChar.Month;
        // 日柱
        var day = eightChar.Day;
        // 时柱
        var hour = eightChar.Hour;

        // 日元(日主、日干)
        var me = day.HeavenStem;

        // 年柱天干十神
        Assert.Equal("正财", me.GetTenStar(year.HeavenStem).GetName());
        // 月柱天干十神
        Assert.Equal("比肩", me.GetTenStar(month.HeavenStem).GetName());
        // 时柱天干十神
        Assert.Equal("七杀", me.GetTenStar(hour.HeavenStem).GetName());

        // 年柱地支十神（本气)
        Assert.Equal("伤官", me.GetTenStar(year.EarthBranch.HideHeavenStemMain).GetName());
        // 年柱地支十神（中气)
        Assert.Equal("正财", me.GetTenStar(year.EarthBranch.HideHeavenStemMiddle).GetName());
        // 年柱地支十神（余气)
        Assert.Equal("正官", me.GetTenStar(year.EarthBranch.HideHeavenStemResidual).GetName());

        // 日柱地支十神（本气)
        Assert.Equal("偏印", me.GetTenStar(day.EarthBranch.HideHeavenStemMain).GetName());
        // 日柱地支藏干（中气)
        Assert.Null(day.EarthBranch.HideHeavenStemMiddle);
        // 日柱地支藏干（余气)
        Assert.Null(day.EarthBranch.HideHeavenStemResidual);

        // 指定任意天干的十神
        Assert.Equal("正财", me.GetTenStar(HeavenStem.FromName("丙")).GetName());
    }

    /// <summary>
    /// 地势(长生十二神)
    /// </summary>
    [Fact]
    public void Test2()
    {
        // 八字
        var eightChar = new EightChar("丙寅", "癸巳", "癸酉", "己未");

        // 年柱
        var year = eightChar.Year;
        // 月柱
        var month = eightChar.Month;
        // 日柱
        var day = eightChar.Day;
        // 时柱
        var hour = eightChar.Hour;

        // 日元(日主、日干)
        var me = day.HeavenStem;

        // 年柱地势
        Assert.Equal("沐浴", me.GetTerrain(year.EarthBranch).GetName());
        // 月柱地势
        Assert.Equal("胎", me.GetTerrain(month.EarthBranch).GetName());
        // 日柱地势
        Assert.Equal("病", me.GetTerrain(day.EarthBranch).GetName());
        // 时柱地势
        Assert.Equal("墓", me.GetTerrain(hour.EarthBranch).GetName());
    }

    /// <summary>
    /// 胎元/胎息/命宫
    /// </summary>
    [Fact]
    public void Test3()
    {
        // 八字
        var eightChar = new EightChar("癸卯", "辛酉", "己亥", "癸酉");

        // 胎元
        var taiYuan = eightChar.FetalOrigin;
        Assert.Equal("壬子", taiYuan.GetName());
        // 胎元纳音
        Assert.Equal("桑柘木", taiYuan.Sound.GetName());
    }

    /// <summary>
    /// 胎息
    /// </summary>
    [Fact]
    public void Test4()
    {
        // 八字
        var eightChar = new EightChar("癸卯", "辛酉", "己亥", "癸酉");

        // 胎息
        var taiXi = eightChar.FetalBreath;
        Assert.Equal("甲寅", taiXi.GetName());
        // 胎息纳音
        Assert.Equal("大溪水", taiXi.Sound.GetName());
    }

    /// <summary>
    /// 命宫
    /// </summary>
    [Fact]
    public void Test5()
    {
        // 八字
        var eightChar = new EightChar("癸卯", "辛酉", "己亥", "癸酉");

        // 命宫
        var mingGong = eightChar.OwnSign;
        Assert.Equal("癸亥", mingGong.GetName());
        // 命宫纳音
        Assert.Equal("大海水", mingGong.Sound.GetName());
    }

    /// <summary>
    /// 身宫
    /// </summary>
    [Fact]
    public void Test6()
    {
        // 八字
        var eightChar = new EightChar("癸卯", "辛酉", "己亥", "癸酉");

        // 身宫
        var shenGong = eightChar.BodySign;
        Assert.Equal("己未", shenGong.GetName());
        // 身宫纳音
        Assert.Equal("天上火", shenGong.Sound.GetName());
    }

    /// <summary>
    /// 地势(长生十二神)
    /// </summary>
    [Fact]
    public void Test7()
    {
        // 八字
        var eightChar = new EightChar("乙酉", "戊子", "辛巳", "壬辰");

        // 日干
        var me = eightChar.Day.HeavenStem;
        // 年柱地势
        Assert.Equal("临官", me.GetTerrain(eightChar.Year.EarthBranch).GetName());
        // 月柱地势
        Assert.Equal("长生", me.GetTerrain(eightChar.Month.EarthBranch).GetName());
        // 日柱地势
        Assert.Equal("死", me.GetTerrain(eightChar.Day.EarthBranch).GetName());
        // 时柱地势
        Assert.Equal("墓", me.GetTerrain(eightChar.Hour.EarthBranch).GetName());
    }

    /// <summary>
    /// 公历时刻转八字
    /// </summary>
    [Fact]
    public void Test8()
    {
        var eightChar = SolarTime.FromYmdHms(2005, 12, 23, 8, 37, 0).GetLunarHour().EightChar;
        Assert.Equal("乙酉", eightChar.Year.GetName());
        Assert.Equal("戊子", eightChar.Month.GetName());
        Assert.Equal("辛巳", eightChar.Day.GetName());
        Assert.Equal("壬辰", eightChar.Hour.GetName());
    }

    [Fact]
    public void Test9()
    {
        var eightChar = SolarTime.FromYmdHms(1988, 2, 15, 23, 30, 0).GetLunarHour().EightChar;
        Assert.Equal("戊辰", eightChar.Year.GetName());
        Assert.Equal("甲寅", eightChar.Month.GetName());
        Assert.Equal("辛丑", eightChar.Day.GetName());
        Assert.Equal("戊子", eightChar.Hour.GetName());
    }

    /// <summary>
    /// 童限测试
    /// </summary>
    [Fact]
    public void Test11()
    {
        var childLimit = ChildLimit.FromSolarTime(SolarTime.FromYmdHms(2022, 3, 9, 20, 51, 0), Gender.Man);
        Assert.Equal(8, childLimit.YearCount);
        Assert.Equal(9, childLimit.MonthCount);
        Assert.Equal(2, childLimit.DayCount);
        Assert.Equal(10, childLimit.HourCount);
        Assert.Equal(26, childLimit.MinuteCount);
        Assert.Equal("2030年12月12日 07:17:00", childLimit.EndTime.ToString());
    }

    /// <summary>
    /// 童限测试
    /// </summary>
    [Fact]
    public void Test12()
    {
        var childLimit = ChildLimit.FromSolarTime(SolarTime.FromYmdHms(2018, 6, 11, 9, 30, 0), Gender.Woman);
        Assert.Equal(1, childLimit.YearCount);
        Assert.Equal(9, childLimit.MonthCount);
        Assert.Equal(10, childLimit.DayCount);
        Assert.Equal(1, childLimit.HourCount);
        Assert.Equal(42, childLimit.MinuteCount);
        Assert.Equal("2020年3月21日 11:12:00", childLimit.EndTime.ToString());
    }

    /// <summary>
    /// 大运测试
    /// </summary>
    [Fact]
    public void Test13()
    {
        // 童限
        var childLimit = ChildLimit.FromSolarTime(SolarTime.FromYmdHms(1983, 2, 15, 20, 0, 0), Gender.Woman);
        // 八字
        Assert.Equal("癸亥 甲寅 甲戌 甲戌", childLimit.EightChar.ToString());
        // 童限年数
        Assert.Equal(6, childLimit.YearCount);
        // 童限月数
        Assert.Equal(2, childLimit.MonthCount);
        // 童限日数
        Assert.Equal(18, childLimit.DayCount);
        // 童限结束(即开始起运)的公历时刻
        Assert.Equal("1989年5月4日 18:24:00", childLimit.EndTime.ToString());
        // 童限开始(即出生)的农历年干支
        Assert.Equal("癸亥", childLimit.StartTime.GetLunarHour().LunarDay.LunarMonth.LunarYear.SixtyCycle.GetName());
        // 童限结束(即开始起运)的农历年干支
        Assert.Equal("己巳", childLimit.EndTime.GetLunarHour().LunarDay.LunarMonth.LunarYear.SixtyCycle.GetName());

        // 第1轮大运
        var decadeFortune = childLimit.StartDecadeFortune;
        // 开始年龄
        Assert.Equal(7, decadeFortune.StartAge);
        // 结束年龄
        Assert.Equal(16, decadeFortune.EndAge);
        // 开始年
        Assert.Equal(1989, decadeFortune.StartLunarYear.Year);
        // 结束年
        Assert.Equal(1998, decadeFortune.EndLunarYear.Year);
        // 干支
        Assert.Equal("乙卯", decadeFortune.GetName());
        // 下一大运
        Assert.Equal("丙辰", decadeFortune.Next(1).GetName());
        // 上一大运
        Assert.Equal("甲寅", decadeFortune.Next(-1).GetName());
        // 第9轮大运
        Assert.Equal("癸亥", decadeFortune.Next(8).GetName());

        // 小运
        var fortune = childLimit.StartFortune;
        // 年龄
        Assert.Equal(7, fortune.Age);
        // 农历年
        Assert.Equal(1989, fortune.LunarYear.Year);
        // 干支
        Assert.Equal("辛巳", fortune.GetName());

        // 流年
        Assert.Equal("己巳", fortune.LunarYear.SixtyCycle.GetName());
    }

    [Fact]
    public void Test14()
    {
        // 童限
        var childLimit = ChildLimit.FromSolarTime(SolarTime.FromYmdHms(1992, 2, 2, 12, 0, 0), Gender.Man);
        // 八字
        Assert.Equal("辛未 辛丑 戊申 戊午", childLimit.EightChar.ToString());
        // 童限年数
        Assert.Equal(9, childLimit.YearCount);
        // 童限月数
        Assert.Equal(0, childLimit.MonthCount);
        // 童限日数
        Assert.Equal(9, childLimit.DayCount);
        // 童限结束(即开始起运)的公历时刻
        Assert.Equal("2001年2月11日 18:58:00", childLimit.EndTime.ToString());
        // 童限开始(即出生)的农历年干支
        Assert.Equal("辛未", childLimit.StartTime.GetLunarHour().LunarDay.LunarMonth.LunarYear.SixtyCycle.GetName());
        // 童限结束(即开始起运)的农历年干支
        Assert.Equal("辛巳", childLimit.EndTime.GetLunarHour().LunarDay.LunarMonth.LunarYear.SixtyCycle.GetName());

        // 第1轮大运
        var decadeFortune = childLimit.StartDecadeFortune;
        // 开始年龄
        Assert.Equal(10, decadeFortune.StartAge);
        // 结束年龄
        Assert.Equal(19, decadeFortune.EndAge);
        // 开始年
        Assert.Equal(2000, decadeFortune.StartLunarYear.Year);
        // 结束年
        Assert.Equal(2009, decadeFortune.EndLunarYear.Year);
        // 干支
        Assert.Equal("庚子", decadeFortune.GetName());
        // 下一大运
        Assert.Equal("己亥", decadeFortune.Next(1).GetName());

        // 小运
        var fortune = childLimit.StartFortune;
        // 年龄
        Assert.Equal(10, fortune.Age);
        // 农历年
        Assert.Equal(2000, fortune.LunarYear.Year);
        // 干支
        Assert.Equal("戊申", fortune.GetName());
        // 小运推移
        Assert.Equal("丙午", fortune.Next(2).GetName());
        Assert.Equal("庚戌", fortune.Next(-2).GetName());

        // 流年
        Assert.Equal("庚辰", fortune.LunarYear.SixtyCycle.GetName());
    }

    /// <summary>
    /// 排盘示例
    /// </summary>
    [Fact]
    public void Test15()
    {
        var eightChar = new EightChar("丙寅", "癸巳", "癸酉", "己未");
        var year = eightChar.Year;
        var month = eightChar.Month;
        var day = eightChar.Day;
        var hour = eightChar.Hour;

        var me = day.HeavenStem;
        _testOutputHelper.WriteLine(
            $"主星：{me.GetTenStar(year.HeavenStem)} {me.GetTenStar(month.HeavenStem)} 日主 {me.GetTenStar(hour.HeavenStem)}");
        _testOutputHelper.WriteLine($"八字：{year} {month} {day} {hour}");
        _testOutputHelper.WriteLine(
            $"藏干：[{year.EarthBranch.HideHeavenStemMain} {year.EarthBranch.HideHeavenStemMiddle} {year.EarthBranch.HideHeavenStemResidual}] [{month.EarthBranch.HideHeavenStemMain} {month.EarthBranch.HideHeavenStemMiddle} {month.EarthBranch.HideHeavenStemResidual}] [{day.EarthBranch.HideHeavenStemMain} {day.EarthBranch.HideHeavenStemMiddle} {day.EarthBranch.HideHeavenStemResidual}] [{hour.EarthBranch.HideHeavenStemMain} {hour.EarthBranch.HideHeavenStemMiddle} {hour.EarthBranch.HideHeavenStemResidual}]");
        _testOutputHelper.WriteLine(
            $"副星：[{me.GetTenStar(year.EarthBranch.HideHeavenStemMain)} {me.GetTenStar(year.EarthBranch.HideHeavenStemMiddle)} {me.GetTenStar(year.EarthBranch.HideHeavenStemResidual)}] [{me.GetTenStar(month.EarthBranch.HideHeavenStemMain)} {me.GetTenStar(month.EarthBranch.HideHeavenStemMiddle)} {me.GetTenStar(month.EarthBranch.HideHeavenStemResidual)}] [{me.GetTenStar(day.EarthBranch.HideHeavenStemMain)} {me.GetTenStar(day.EarthBranch.HideHeavenStemMiddle)} {me.GetTenStar(day.EarthBranch.HideHeavenStemResidual)}] [{me.GetTenStar(hour.EarthBranch.HideHeavenStemMain)} {me.GetTenStar(hour.EarthBranch.HideHeavenStemMiddle)} {me.GetTenStar(hour.EarthBranch.HideHeavenStemResidual)}]");
        _testOutputHelper.WriteLine(
            $"五行：{year.HeavenStem.Element}{year.EarthBranch.Element} {month.HeavenStem.Element}{month.EarthBranch.Element} {day.HeavenStem.Element}{day.EarthBranch.Element} {hour.HeavenStem.Element}{hour.EarthBranch.Element}");
        _testOutputHelper.WriteLine($"纳音：{year.Sound} {month.Sound} {day.Sound} {hour.Sound}");
        _testOutputHelper.WriteLine(
            $"星运：{me.GetTerrain(year.EarthBranch)} {me.GetTerrain(month.EarthBranch)} {me.GetTerrain(day.EarthBranch)} {me.GetTerrain(hour.EarthBranch)}");
        _testOutputHelper.WriteLine(
            $"自坐：{year.HeavenStem.GetTerrain(year.EarthBranch)} {month.HeavenStem.GetTerrain(month.EarthBranch)} {day.HeavenStem.GetTerrain(day.EarthBranch)} {hour.HeavenStem.GetTerrain(hour.EarthBranch)}");
        _testOutputHelper.WriteLine($"纳音：{year.Sound} {month.Sound} {day.Sound} {hour.Sound}");

        var yearExtraEarthBranches = string.Join(",", year.ExtraEarthBranches.Select(o => o.ToString()));
        var monthExtraEarthBranches = string.Join(",", month.ExtraEarthBranches.Select(o => o.ToString()));
        var dayExtraEarthBranches = string.Join(",", day.ExtraEarthBranches.Select(o => o.ToString()));
        var hourExtraEarthBranches = string.Join(",", hour.ExtraEarthBranches.Select(o => o.ToString()));
        _testOutputHelper.WriteLine(
            $"空亡：{yearExtraEarthBranches} {monthExtraEarthBranches} {dayExtraEarthBranches} {hourExtraEarthBranches}");
        
        _testOutputHelper.WriteLine($"胎元：{eightChar.FetalOrigin}({eightChar.FetalOrigin.Sound})");
        _testOutputHelper.WriteLine($"胎息：{eightChar.FetalBreath}({eightChar.FetalBreath.Sound})");
        _testOutputHelper.WriteLine($"命宫：{eightChar.OwnSign}({eightChar.OwnSign.Sound})");
        _testOutputHelper.WriteLine($"身宫：{eightChar.BodySign}({eightChar.BodySign.Sound})");
    }

    [Fact]
    public void Test16()
    {
        // 童限
        var childLimit = ChildLimit.FromSolarTime(SolarTime.FromYmdHms(1990, 3, 15, 10, 30, 0), Gender.Man);
        // 八字
        Assert.Equal("庚午 己卯 己卯 己巳", childLimit.EightChar.ToString());
        // 童限年数
        Assert.Equal(6, childLimit.YearCount);
        // 童限月数
        Assert.Equal(11, childLimit.MonthCount);
        // 童限日数
        Assert.Equal(23, childLimit.DayCount);
        // 童限结束(即开始起运)的公历时刻
        Assert.Equal("1997年3月11日 00:22:00", childLimit.EndTime.ToString());

        // 小运
        var fortune = childLimit.StartFortune;
        // 年龄
        Assert.Equal(8, fortune.Age);
    }

    [Fact]
    public void Test17()
    {
        Assert.Equal("丁丑", new EightChar("己丑", "戊辰", "戊辰", "甲子").OwnSign.GetName());
    }

    [Fact]
    public void Test18()
    {
        Assert.Equal("乙卯", new EightChar("戊戌", "庚申", "丁亥", "丙午").OwnSign.GetName());
    }

    [Fact]
    public void Test19()
    {
        Assert.Equal("甲戌",
            new EightChar(SixtyCycle.FromName("甲子"), SixtyCycle.FromName("壬申"), null, SixtyCycle.FromName("乙亥")).OwnSign
                .GetName());
    }

    [Fact]
    public void Test20()
    {
        var eightChar = ChildLimit.FromSolarTime(SolarTime.FromYmdHms(2024, 1, 29, 9, 33, 0), Gender.Man)
            .EightChar;
        Assert.Equal("癸亥", eightChar.OwnSign.GetName());
        Assert.Equal("己未", eightChar.BodySign.GetName());
    }

    [Fact]
    public void Test21()
    {
        Assert.Equal("庚子",
            new EightChar(SixtyCycle.FromName("辛亥"), SixtyCycle.FromName("乙未"), null, SixtyCycle.FromName("甲辰"))
                .BodySign.GetName());
    }

    [Fact]
    public void Test22()
    {
        Assert.Equal("丙寅",
            ChildLimit.FromSolarTime(SolarTime.FromYmdHms(1990, 1, 27, 0, 0, 0), Gender.Man).EightChar.BodySign
                .GetName());
    }

    [Fact]
    public void Test23()
    {
        Assert.Equal("甲戌",
            ChildLimit.FromSolarTime(SolarTime.FromYmdHms(2019, 3, 7, 8, 0, 0), Gender.Man).EightChar.OwnSign
                .GetName());
    }

    [Fact]
    public void Test24()
    {
        Assert.Equal("丁丑",
            ChildLimit.FromSolarTime(SolarTime.FromYmdHms(2019, 3, 27, 2, 0, 0), Gender.Man).EightChar.OwnSign
                .GetName());
    }

    [Fact]
    public void Test25()
    {
        Assert.Equal("丙寅", LunarHour.FromYmdHms(1994, 5, 20, 18, 0, 0).EightChar.OwnSign.GetName());
    }

    [Fact]
    public void Test26()
    {
        Assert.Equal("己丑", SolarTime.FromYmdHms(1986, 5, 29, 13, 37, 0).GetLunarHour().EightChar.BodySign.GetName());
    }

    [Fact]
    public void Test27()
    {
        Assert.Equal("乙丑", SolarTime.FromYmdHms(1994, 12, 6, 2, 0, 0).GetLunarHour().EightChar.BodySign.GetName());
    }

    [Fact]
    public void Test28()
    {
        Assert.Equal("辛卯", new EightChar("辛亥", "丁酉", "丙午", "癸巳").OwnSign.GetName());
    }

    [Fact]
    public void Test29()
    {
        var eightChar = new EightChar("丙寅", "庚寅", "辛卯", "壬辰");
        Assert.Equal("己亥", eightChar.OwnSign.GetName());
        Assert.Equal("乙未", eightChar.BodySign.GetName());
    }

    [Fact]
    public void Test30()
    {
        Assert.Equal("乙巳", new EightChar("壬子", "辛亥", "壬戌", "乙巳").BodySign.GetName());
    }

    [Fact]
    public void Test31()
    {
        var solarTimes = new EightChar("丙辰", "丁酉", "丙子", "甲午").GetSolarTimes(1900, 2024);
        var actual = solarTimes.Select(solarTime => solarTime.ToString()).ToList();

        var expected = new List<string>
        {
            "1916年10月6日 12:00:00",
            "1976年9月21日 12:00:00"
        };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test32()
    {
        var solarTimes = new EightChar("壬寅", "庚戌", "己未", "乙亥").GetSolarTimes(1900, 2024);
        var actual = solarTimes.Select(solarTime => solarTime.ToString()).ToList();

        var expected = new List<string> { "2022年11月2日 22:00:00" };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test33()
    {
        var solarTimes = new EightChar("己卯", "辛未", "甲戌", "壬申").GetSolarTimes(1900, 2024);
        var actual = solarTimes.Select(solarTime => solarTime.ToString()).ToList();

        var expected = new List<string>
        {
            "1939年8月5日 16:00:00",
            "1999年7月21日 16:00:00"
        };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test34()
    {
        var solarTimes = new EightChar("庚子", "戊子", "己卯", "庚午").GetSolarTimes(1900, 2024);
        var actual = solarTimes.Select(solarTime => solarTime.ToString()).ToList();

        var expected = new List<string>
        {
            "1901年1月1日 12:00:00",
            "1960年12月17日 12:00:00"
        };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test35()
    {
        var solarTimes = new EightChar("庚子", "癸未", "乙丑", "丁亥").GetSolarTimes(1900, 2024);
        var actual = solarTimes.Select(solarTime => solarTime.ToString()).ToList();

        var expected = new List<string>
        {
            "1960年8月5日 22:00:00",
            "2020年7月21日 22:00:00"
        };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test36()
    {
        var solarTimes = new EightChar("癸卯", "甲寅", "甲寅", "甲子").GetSolarTimes(1800, 2024);
        var actual = solarTimes.Select(solarTime => solarTime.ToString()).ToList();

        var expected = new List<string>
        {
            "1843年2月9日 00:00:00",
            "2023年2月25日 00:00:00"
        };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test37()
    {
        var solarTimes = new EightChar("甲辰", "丙寅", "己亥", "戊辰").GetSolarTimes(1800, 2024);
        var actual = solarTimes.Select(solarTime => solarTime.ToString()).ToList();

        var expected = new List<string>
        {
            "1964年2月20日 08:00:00",
            "2024年2月5日 08:00:00"
        };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test38()
    {
        var solarTimes = new EightChar("己亥", "丁丑", "壬寅", "戊申").GetSolarTimes(1900, 2024);
        var actual = solarTimes.Select(solarTime => solarTime.ToString()).ToList();

        var expected = new List<string>
        {
            "1900年1月29日 16:00:00",
            "1960年1月15日 16:00:00"
        };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test39()
    {
        var solarTimes = new EightChar("己亥", "丙子", "癸酉", "庚申").GetSolarTimes(1900, 2024);
        var actual = solarTimes.Select(solarTime => solarTime.ToString()).ToList();

        var expected = new List<string> { "1959年12月17日 16:00:00" };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test40()
    {
        var solarTimes = new EightChar("丁丑", "癸卯", "癸丑", "辛酉").GetSolarTimes(1900, 2024);
        var actual = solarTimes.Select(solarTime => solarTime.ToString()).ToList();

        var expected = new List<string>
        {
            "1937年3月27日 18:00:00",
            "1997年3月12日 18:00:00"
        };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test41()
    {
        var solarTimes = new EightChar("乙未", "己卯", "丁丑", "甲辰").GetSolarTimes(1900, 2024);
        var actual = solarTimes.Select(solarTime => solarTime.ToString()).ToList();

        var expected = new List<string> { "1955年3月17日 08:00:00" };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test42()
    {
        Assert.Equal("壬申", new EightChar("甲辰", "丙寅", "己亥", "辛未").OwnSign.GetName());
    }

    [Fact]
    public void Test43()
    {
        // 采用元亨利贞的起运算法
        ChildLimit.Provider = new China95ChildLimitProvider();
        // 童限
        var childLimit = ChildLimit.FromSolarTime(SolarTime.FromYmdHms(1986, 5, 29, 13, 37, 0), Gender.Man);
        // 童限年数
        Assert.Equal(2, childLimit.YearCount);
        // 童限月数
        Assert.Equal(7, childLimit.MonthCount);
        // 童限日数
        Assert.Equal(0, childLimit.DayCount);
        // 童限时数
        Assert.Equal(0, childLimit.HourCount);
        // 童限分数
        Assert.Equal(0, childLimit.MinuteCount);
        // 童限结束(即开始起运)的公历时刻
        Assert.Equal("1988年12月29日 13:37:00", childLimit.EndTime.ToString());

        // 为了不影响其他测试用例，恢复默认起运算法
        ChildLimit.Provider = new DefaultChildLimitProvider();
    }
    
    [Fact]
    public void Test46()
    {
        LunarHour.Provider = new LunarSect2EightCharProvider();
        
        var solarTimes = new EightChar("壬寅", "丙午", "己亥", "丙子").GetSolarTimes(1900, 2024);
        var actual = solarTimes.Select(solarTime => solarTime.ToString()).ToList();

        var expected = new List<string> { "1962年6月30日 23:00:00", "2022年6月15日 23:00:00" };
        Assert.Equal(expected, actual);

        LunarHour.Provider = new DefaultEightCharProvider();
    }
}