using tyme.evt;

namespace test;

/// <summary>
/// 事件测试
/// </summary>
public class EventTest
{
    void Init()
    {
        // 公历现代节日
        // EventManager.Data = "@0VV__0Ux公历现代节日:元旦@0Xc__0Ux公历现代节日:妇女节@0Xg__0_Q公历现代节日:植树节@0ZV__0Ux公历现代节日:劳动节@0ZY__0Ux公历现代节日:青年节@0aV__0Ux公历现代节日:儿童节@0bV__0Uo公历现代节日:建党节@0cV__0Ug公历现代节日:建军节@0de__0_V公历现代节日:教师节@0eV__0Ux公历现代节日:国庆节";
        EventManager.Update("公历现代节日:元旦", Event.Builder().SolarDay(1, 1, 0).StartYear(1950).Build());
        EventManager.Update("公历现代节日:妇女节", Event.Builder().SolarDay(3, 8, 0).StartYear(1950).Build());
        EventManager.Update("公历现代节日:植树节", Event.Builder().SolarDay(3, 12, 0).StartYear(1979).Build());
        EventManager.Update("公历现代节日:劳动节", Event.Builder().SolarDay(5, 1, 0).StartYear(1950).Build());
        EventManager.Update("公历现代节日:青年节", Event.Builder().SolarDay(5, 4, 0).StartYear(1950).Build());
        EventManager.Update("公历现代节日:儿童节", Event.Builder().SolarDay(6, 1, 0).StartYear(1950).Build());
        EventManager.Update("公历现代节日:建党节", Event.Builder().SolarDay(7, 1, 0).StartYear(1941).Build());
        EventManager.Update("公历现代节日:建军节", Event.Builder().SolarDay(8, 1, 0).StartYear(1933).Build());
        EventManager.Update("公历现代节日:教师节", Event.Builder().SolarDay(9, 10, 0).StartYear(1985).Build());
        EventManager.Update("公历现代节日:国庆节", Event.Builder().SolarDay(10, 1, 0).StartYear(1950).Build());

        // 农历传统节日
        // EventManager.Data = "@2VV__000农历传统节日:春节@2Vj__000农历传统节日:元宵节@2WW__000农历传统节日:龙头节@2XX__000农历传统节日:上巳节@3b___000农历传统节日:清明节@2ZZ__000农历传统节日:端午节@2bb__000农历传统节日:七夕节@2bj__000农历传统节日:中元节@2cj__000农历传统节日:中秋节@2dd__000农历传统节日:重阳节@3s___000农历传统节日:冬至节@2gc__000农历传统节日:腊八节@2hV_U000农历传统节日:除夕";
        EventManager.Update("农历传统节日:春节", Event.Builder().LunarDay(1, 1, 0).Build());
        EventManager.Update("农历传统节日:元宵节", Event.Builder().LunarDay(1, 15, 0).Build());
        EventManager.Update("农历传统节日:龙头节", Event.Builder().LunarDay(2, 2, 0).Build());
        EventManager.Update("农历传统节日:上巳节", Event.Builder().LunarDay(3, 3, 0).Build());
        EventManager.Update("农历传统节日:清明节", Event.Builder().TermDay(7, 0).Build());
        EventManager.Update("农历传统节日:端午节", Event.Builder().LunarDay(5, 5, 0).Build());
        EventManager.Update("农历传统节日:七夕节", Event.Builder().LunarDay(7, 7, 0).Build());
        EventManager.Update("农历传统节日:中元节", Event.Builder().LunarDay(7, 15, 0).Build());
        EventManager.Update("农历传统节日:中秋节", Event.Builder().LunarDay(8, 15, 0).Build());
        EventManager.Update("农历传统节日:重阳节", Event.Builder().LunarDay(9, 9, 0).Build());
        EventManager.Update("农历传统节日:冬至节", Event.Builder().TermDay(24, 0).Build());
        EventManager.Update("农历传统节日:腊八节", Event.Builder().LunarDay(12, 8, 0).Build());
        EventManager.Update("农历传统节日:除夕", Event.Builder().LunarDay(13, 1, 0).Offset(-1).Build());

        EventManager.Update("情人节", Event.Builder().SolarDay(2, 14, 0).StartYear(270).Build());
        EventManager.Update("国际消费者权益日", Event.Builder().SolarDay(3, 15, 0).StartYear(1983).Build());
        EventManager.Update("愚人节", Event.Builder().SolarDay(4, 1, 0).StartYear(1564).Build());
        EventManager.Update("万圣夜", Event.Builder().SolarDay(10, 31, 0).StartYear(600).Build());
        EventManager.Update("万圣节", Event.Builder().SolarDay(11, 1, 0).StartYear(600).Build());
        EventManager.Update("平安夜", Event.Builder().SolarDay(12, 24, 0).StartYear(336).Build());
        EventManager.Update("圣诞节", Event.Builder().SolarDay(12, 25, 0).StartYear(336).Build());

        EventManager.Update("全国中小学生安全教育日", Event.Builder().SolarWeek(3, -1, 1).StartYear(1996).Build());
        EventManager.Update("母亲节", Event.Builder().SolarWeek(5, 2, 0).StartYear(1914).Build());
        EventManager.Update("父亲节", Event.Builder().SolarWeek(6, 3, 0).StartYear(1972).Build());
        EventManager.Update("感恩节", Event.Builder().SolarWeek(11, 4, 4).StartYear(1941).Build());

        // 清明前1天
        EventManager.Update("寒食节", Event.Builder().TermDay(7, -1).Build());
        // 立春后第5个戊日
        EventManager.Update("春社", Event.Builder().TermHeavenStem(3, 4, 30).Offset(10).Build());
        // 立秋后第5个戊日
        EventManager.Update("秋社", Event.Builder().TermHeavenStem(15, 4, 30).Offset(10).Build());

        // 夏至后第3个庚日
        EventManager.Update("入伏", Event.Builder().TermHeavenStem(12, 6, 20).Build());
        // 夏至后第4个庚日
        EventManager.Update("中伏", Event.Builder().TermHeavenStem(12, 6, 30).Build());
        // 立秋后第1个庚日
        EventManager.Update("末伏", Event.Builder().TermHeavenStem(15, 6, 0).Build());

        // 芒种后第1个丙日
        EventManager.Update("入梅", Event.Builder().TermHeavenStem(11, 2, 0).Build());
        // 小暑后第1个未日
        EventManager.Update("出梅", Event.Builder().TermEarthBranch(13, 7, 0).Build());

        // 如果没有2月29，则倒推1天
        EventManager.Update("公历生日", Event.Builder().SolarDay(2, 29, -1).StartYear(2004).Build());

        EventManager.Update("农历生日", Event.Builder().LunarDay(4, 21, 0).StartYear(1986).Build());
    }

    [Fact]
    public void Test0()
    {
        Init();
        var e = Event.FromName("公历生日");

        var d = e.GetSolarDay(2008);
        Assert.Equal("2008年2月29日", d.ToString());

        // 2005年没有2月29，按最初设置的，没有就倒推1天
        d = e.GetSolarDay(2005);
        Assert.Equal("2005年2月28日", d.ToString());

        e = Event.FromName("农历生日");
        d = e.GetSolarDay(2026);
        Assert.Equal("2026年6月6日", d.ToString());
    }

    [Fact]
    public void Test1()
    {
        Init();
        var e = Event.FromName("公历生日");

        var d = e.GetSolarDay(1985);
        Assert.Null(d);
    }

    [Fact]
    public void Test2()
    {
        Init();
        var e = Event.FromName("国际消费者权益日");
        Assert.NotNull(e);
        Assert.Equal("国际消费者权益日", e.GetName());

        var d = e.GetSolarDay(2026);
        Assert.Equal("2026年3月15日", d.ToString());

        List<Event> events = Event.FromSolarDay(d);
        Assert.Equal([e], events);
    }
}