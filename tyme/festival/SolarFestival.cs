using tyme.enums;
using tyme.evt;
using tyme.solar;

namespace tyme.festival
{
    /// <summary>
    /// 公历现代节日
    /// </summary>
    public class SolarFestival : AbstractFestival
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names = { "元旦", "妇女节", "植树节", "劳动节", "青年节", "儿童节", "建党节", "建军节", "教师节", "国庆节" };

        /// <summary>
        /// 数据
        /// </summary>
        public static string Data = "0VV__0Ux0Xc__0Ux0Xg__0_Q0ZV__0Ux0ZY__0Ux0aV__0Ux0bV__0Uo0cV__0Ug0de__0_V0eV__0Ux";


        /// <summary>
        /// 公历日
        /// </summary>
        public SolarDay Day => base.Day as SolarDay;

        /// <summary>
        /// 起始年
        /// </summary>
        public int StartYear => Event.StartYear;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="type">节日类型</param>
        /// <param name="index">索引</param>
        /// <param name="e">事件</param>
        /// <param name="day">公历日</param>
        public SolarFestival(FestivalType type, int index, Event e, SolarDay day) : base(type, index, e, day)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="index">索引值</param>
        /// <returns>公历现代节日</returns>
        public static SolarFestival FromIndex(int year, int index)
        {
            if (index < 0 || index >= Names.Length) {
                return null;
            }
            var start = index * 8;
            var e = new Event(Names[index], "@" + Data.Substring(start, 8));
            return year < e.StartYear ? null : new SolarFestival(FestivalType.Day, index, e, SolarDay.FromYmd(year, e.GetValue(2), e.GetValue(3)));
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="month">公历月</param>
        /// <param name="day">公历日</param>
        /// <returns>公历现代节日</returns>
        public static SolarFestival FromYmd(int year, int month, int day)
        {
            var d = SolarDay.FromYmd(year, month, day);
            for (int i = 0, j = Names.Length; i < j; i++) {
                var start = i * 8;
                var e = new Event(Names[i], "@" + Data.Substring(start, 8));
                if (d.Year >= e.StartYear && d.Month == e.GetValue(2) && d.Day == e.GetValue(3)) {
                    return new SolarFestival(FestivalType.Day, i, e, d);
                }
            }
            return null;
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的公历现代节日</returns>
        public new SolarFestival Next(int n)
        {
            var size = Names.Length;
            var i = Index + n;
            return FromIndex((Day.Year * size + i) / size, IndexOf(i, size));
        }
    }
}