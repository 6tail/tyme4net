using System;
using tyme.enums;
using tyme.evt;
using tyme.lunar;
using tyme.solar;

namespace tyme.festival
{
    /// <summary>
    /// 农历传统节日（依据国家标准《农历的编算和颁行》GB/T 33661-2017）
    /// </summary>
    public class LunarFestival : AbstractFestival
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names = { "春节", "元宵节", "龙头节", "上巳节", "清明节", "端午节", "七夕节", "中元节", "中秋节", "重阳节", "冬至节", "腊八节", "除夕" };

        /// <summary>
        /// 数据
        /// </summary>
        public static string Data = "2VV__0002Vj__0002WW__0002XX__0003b___0002ZZ__0002bb__0002bj__0002cj__0002dd__0003s___0002gc__0002hV_U000";

        /// <summary>
        /// 农历日
        /// </summary>
        public LunarDay Day => base.Day as LunarDay;

        /// <summary>
        /// 节气
        /// </summary>
        public SolarTerm SolarTerm
        {
            get
            {
                var t = Day.GetSolarDay().TermDay;
                return t.DayIndex == 0 ? t.SolarTerm : null;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="type">节日类型</param>
        /// <param name="index">索引</param>
        /// <param name="e">事件</param>
        /// <param name="day">农历日</param>
        public LunarFestival(FestivalType type, int index, Event e, LunarDay day): base(type, index, e, day)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="index">索引值</param>
        /// <returns>农历传统节日</returns>
        /// <exception cref="ArgumentException"></exception>
        public static LunarFestival FromIndex(int year, int index)
        {
            if (index < 0 || index >= Names.Length)
            {
                return null;
            }
            var start = index * 8;
            var e = new Event(Names[index], "@" + Data.Substring(start, 8));
            switch (e.Type) {
                case EventType.LunarDay:
                    var m = e.GetMonth(year);
                    var d = LunarDay.FromYmd(m[0], m[1], e.GetValue(3));
                    var offset = e.GetValue(5);
                    return new LunarFestival(FestivalType.Day, index, e, 0 == offset ? d : d.Next(offset));
                case EventType.TermDay:
                    return new LunarFestival(FestivalType.Term, index, e, SolarTerm.FromIndex(year, e.GetValue(2)).GetSolarDay().GetLunarDay());
                case EventType.SolarDay:
                case EventType.SolarWeek:
                case EventType.TermHs:
                case EventType.TermEb:
                default:
                    return null;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">农历年</param>
        /// <param name="month">农历月</param>
        /// <param name="day">农历日</param>
        /// <returns>农历传统节日</returns>
        public static LunarFestival FromYmd(int year, int month, int day)
        {
            var d = LunarDay.FromYmd(year, month, day);
            for (int i = 0, j = Names.Length; i < j; i++) {
                var start = i * 8;
                var e = new Event(Names[i], '@' + Data.Substring(start, 8));
                switch (e.Type) {
                    case EventType.LunarDay:
                        var offset = e.GetValue(5);
                        if (0 == offset) {
                            if (d.Month == e.GetValue(2) && d.Day == e.GetValue(3)) {
                                return new LunarFestival(FestivalType.Day, i, e, d);
                            }
                        } else {
                            var m = e.GetMonth(d.Year);
                            var next = d.Next(-offset);
                            if (next.Year == m[0] && next.Month == m[1] && next.Day == e.GetValue(3)) {
                                return new LunarFestival(FestivalType.Day, i, e, d);
                            }
                        }
                        break;
                    case EventType.TermDay:
                        var term = d.GetSolarDay().TermDay;
                        if (term.DayIndex == 0 && term.SolarTerm.Index == e.GetValue(2) % 24) {
                            return new LunarFestival(FestivalType.Term, i, e, d);
                        }
                        break;
                    case EventType.SolarDay:
                    case EventType.SolarWeek:
                    case EventType.TermHs:
                    case EventType.TermEb:
                    default:
                        break;
                }
            }
            return null;
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的农历传统节日</returns>
        public new LunarFestival Next(int n)
        {
            var size = Names.Length;
            var i = Index + n;
            return FromIndex((Day.Year * size + i) / size, IndexOf(i, size));
        }
    }
}