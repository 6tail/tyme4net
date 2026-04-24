using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using tyme.enums;
using tyme.lunar;
using tyme.solar;

namespace tyme.evt
{
    /// <summary>
    /// 事件
    /// </summary>
    public class Event : AbstractCulture
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected string name;

        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="data">数据</param>
        /// <exception cref="ArgumentException"></exception>
        public static void Validate(string data)
        {
            if (data == null)
            {
                throw new ArgumentException("illegal event data: null");
            }

            if (data.Length != 9)
            {
                throw new ArgumentException("illegal event data: " + data);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="data">数据</param>
        public Event(string name, string data)
        {
            Validate(data);
            this.name = name;
            Data = data;
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <returns>构造器</returns>
        public static EventBuilder Builder()
        {
            return new EventBuilder();
        }

        /// <summary>
        /// 从名称初始化
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>事件</returns>
        public static Event FromName(string name)
        {
            var matcher = Regex.Match(EventManager.Data, EventManager.RegexExp + name);
            return matcher.Success ? new Event(name, matcher.Groups[1].Value) : null;
        }
        
        internal int GetCharIndex(int index) {
            return EventManager.Chars.IndexOf(Data[index]);
        }
        
        /// <summary>
        /// 从数据取数值
        /// </summary>
        /// <param name="index">下标</param>
        /// <returns>数值</returns>
        public int GetValue(int index) {
            return GetCharIndex(index) - 31;
        }

        /// <summary>
        /// 从数据取年月
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>年月数组</returns>
        public int[] GetMonth(int year) {
            var y = year;
            var m = GetValue(2);
            if (m > 12) {
                m = 1;
                y += 1;
            }
            return new[] {y, m};
        }

        /// <summary>
        /// 事件类型
        /// </summary>
        /// <returns>事件类型</returns>
        public EventType Type
        {
            get
            {
                switch (GetCharIndex(1))
                {
                    case 1:
                        return EventType.SolarWeek;
                    case 2:
                        return EventType.LunarDay;
                    case 3:
                        return EventType.TermDay;
                    case 4:
                        return EventType.TermHs;
                    case 5:
                        return EventType.TermEb;
                    default:
                        return EventType.SolarDay;
                }
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return name;
        }

        /// <summary>
        /// 起始年
        /// </summary>
        /// <returns>年</returns>
        public int StartYear
        {
            get
            {
                var n = 0;
                var size = EventManager.Chars.Length;
                for (var i = 0; i < 3; i++)
                {
                    n = n * size + GetCharIndex(6 + i);
                }

                return n;
            }
        }

        /// <summary>
        /// 指定公历日的事件列表
        /// </summary>
        /// <param name="d">公历日</param>
        /// <returns>事件列表</returns>
        public static List<Event> FromSolarDay(SolarDay d)
        {
            return All().Where(e => d.Equals(e.GetSolarDay(d.Year))).ToList();
        }

        /// <summary>
        /// 所有事件
        /// </summary>
        /// <returns>事件列表</returns>
        public static List<Event> All()
        {
            var matches = Regex.Matches(EventManager.Data, EventManager.RegexExp + "(.[^@]+)");
            return (from Match matcher in matches select new Event(matcher.Groups[2].Value, matcher.Groups[1].Value))
                .ToList();
        }

        /// <summary>
        /// 公历日
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>公历日，如果当年没有该事件，返回null</returns>
        public SolarDay GetSolarDay(int year)
        {
            if (year < StartYear)
            {
                return null;
            }

            SolarDay d;
            switch (Type)
            {
                case EventType.SolarDay:
                    d = GetSolarDayBySolarDay(year);
                    break;
                case EventType.SolarWeek:
                    d = GetSolarDayByWeek(year);
                    break;
                case EventType.LunarDay:
                    d = GetSolarDayByLunarDay(year);
                    break;
                case EventType.TermDay:
                    d = GetSolarDayByTerm(year);
                    break;
                case EventType.TermHs:
                    d = GetSolarDayByTermHeavenStem(year);
                    break;
                case EventType.TermEb:
                    d = GetSolarDayByTermEarthBranch(year);
                    break;
                default:
                    d = null;
                    break;
            }

            if (d == null)
            {
                return null;
            }

            var offset = GetValue(5);
            return offset == 0 ? d : d.Next(offset);
        }

        /// <summary>
        /// 公历日
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>公历日</returns>
        protected SolarDay GetSolarDayBySolarDay(int year)
        {
            var month = GetMonth(year);
            var y = month[0];
            var m = month[1];
            var d = GetValue(3);
            var delay = GetValue(4);
            var lastDay = SolarMonth.FromYm(y, m).DayCount;
            if (d > lastDay)
            {
                if (delay == 0)
                {
                    return null;
                }

                return delay < 0 ? SolarDay.FromYmd(y, m, d + delay) : SolarDay.FromYmd(y, m, lastDay).Next(delay);
            }

            return SolarDay.FromYmd(y, m, d);
        }

        /// <summary>
        /// 农历日
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>公历日</returns>
        protected SolarDay GetSolarDayByLunarDay(int year)
        {
            var month = GetMonth(year);
            var y = month[0];
            var m = month[1];
            var d = GetValue(3);
            var delay = GetValue(4);
            var lastDay = LunarMonth.FromYm(y, m).DayCount;
            if (d > lastDay)
            {
                if (delay == 0)
                {
                    return null;
                }

                return delay < 0 ? LunarDay.FromYmd(y, m, d + delay).GetSolarDay() : LunarDay.FromYmd(y, m, lastDay).GetSolarDay().Next(delay);
            }

            return LunarDay.FromYmd(y, m, d).GetSolarDay();
        }

        /// <summary>
        /// 公历周
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>公历日</returns>
        protected SolarDay GetSolarDayByWeek(int year)
        {
            // 第几个星期
            var n = GetValue(3);
            if (n == 0)
            {
                return null;
            }

            var m = SolarMonth.FromYm(year, GetValue(2));
            // 星期几
            var w = GetValue(4);
            SolarDay d;
            if (n > 0)
            {
                // 当月第1天
                d = m.FirstDay;
                // 往后找第几个星期几
                return d.Next(d.Week.StepsTo(w) + 7 * n - 7);
            }

            // 当月最后一天
            d = SolarDay.FromYmd(year, m.Month, m.DayCount);
            // 往前找第几个星期几
            return d.Next(d.Week.StepsBackTo(w) + 7 * n + 7);
        }

        /// <summary>
        /// 节气
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>公历日</returns>
        protected SolarDay GetSolarDayByTerm(int year)
        {
            var d = SolarTerm.FromIndex(year, GetValue(2)).GetSolarDay();
            var offset = GetValue(4);
            return offset == 0 ? d : d.Next(offset);
        }

        /// <summary>
        /// 节气天干
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>公历日</returns>
        protected SolarDay GetSolarDayByTermHeavenStem(int year)
        {
            var d = GetSolarDayByTerm(year);
            return d.Next(d.GetLunarDay().SixtyCycle.HeavenStem.StepsTo(GetValue(3)));
        }

        /// <summary>
        /// 节气地支
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>公历日</returns>
        protected SolarDay GetSolarDayByTermEarthBranch(int year)
        {
            var d = GetSolarDayByTerm(year);
            return d.Next(d.GetLunarDay().SixtyCycle.EarthBranch.StepsTo(GetValue(3)));
        }
    }
}