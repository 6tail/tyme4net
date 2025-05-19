using System;
using tyme.culture;
using tyme.solar;

namespace tyme.rabbyung
{
    /// <summary>
    /// 藏历日，仅支持藏历1950年十二月初一（公历1951年1月8日）至藏历2050年十二月三十（公历2051年2月11日）
    /// </summary>
    public class RabByungDay : AbstractTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names = { "初一", "初二", "初三", "初四", "初五", "初六", "初七", "初八", "初九", "初十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十", "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十" };

        /// <summary>
        /// 藏历月
        /// </summary>
        public RabByungMonth RabByungMonth { get; }
        
        /// <summary>
        /// 是否闰日
        /// </summary>
        public bool IsLeap { get; }

        /// <summary>
        /// 年
        /// </summary>
        public int Year => RabByungMonth.Year;

        /// <summary>
        /// 月，闰月为负
        /// </summary>
        public int Month => RabByungMonth.MonthWithLeap;
        
        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; }
        
        /// <summary>
        /// 日，当日为闰日时，返回负数
        /// </summary>
        public int DayWithLeap => IsLeap ? -Day : Day;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="month">藏历月</param>
        /// <param name="day">藏历日，闰日为负</param>
        /// <exception cref="ArgumentException"></exception>
        public RabByungDay(RabByungMonth month, int day)
        {
            if (day == 0 || day < -30 || day > 30)
            {
                throw new ArgumentException($"illegal day {day} in {month}");
            }
            var leap = day < 0;
            var d = Math.Abs(day);
            if (leap && !month.LeapDays.Contains(d)) {
                throw new ArgumentException($"illegal leap day {d} in {month}");
            }
            if (!leap && month.MissDays.Contains(d)) {
                throw new ArgumentException($"illegal day {d} in {month}");
            }
            RabByungMonth = month;
            Day = d;
            IsLeap = leap;
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">藏历年</param>
        /// <param name="month">藏历月，闰月为负</param>
        /// <param name="day">藏历日，闰日为负</param>
        public RabByungDay(int year, int month, int day): this(RabByungMonth.FromYm(year, month), day)
        {
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rabByungIndex">饶迥(胜生周)序号，从0开始</param>
        /// <param name="element">藏历五行</param>
        /// <param name="zodiac">生肖</param>
        /// <param name="month">月</param>
        /// <param name="day">藏历日，闰日为负</param>
        public RabByungDay(int rabByungIndex, RabByungElement element, Zodiac zodiac, int month, int day): this(new RabByungMonth(rabByungIndex, element, zodiac, month), day)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">藏历年</param>
        /// <param name="month">藏历月，闰月为负</param>
        /// <param name="day">藏历日，闰日为负</param>
        /// <returns>藏历日</returns>
        public static RabByungDay FromYmd(int year, int month, int day)
        {
            return new RabByungDay(year, month, day);
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rabByungIndex">饶迥(胜生周)序号，从0开始</param>
        /// <param name="element">藏历五行</param>
        /// <param name="zodiac">生肖</param>
        /// <param name="month">月</param>
        /// <param name="day">藏历日，闰日为负</param>
        /// <returns>藏历日</returns>
        public static RabByungDay FromElementZodiac(int rabByungIndex, RabByungElement element, Zodiac zodiac, int month, int day)
        {
            return new RabByungDay(rabByungIndex, element, zodiac, month, day);
        }
        
        /// <summary>
        /// 公历日转藏历日
        /// </summary>
        /// <param name="solarDay">公历日</param>
        /// <returns>藏历日</returns>
        public static RabByungDay FromSolarDay(SolarDay solarDay)
        {
            var days = solarDay.Subtract(SolarDay.FromYmd(1951, 1, 8));
            var m = RabByungMonth.FromYm(1950, 12);
            var count = m.DayCount;
            while (days >= count) {
                days -= count;
                m = m.Next(1);
                count = m.DayCount;
            }
            var day = days + 1;
            foreach (var d in m.SpecialDays) {
                if (d < 0) {
                    if (day >= -d) {
                        day++;
                    }
                } else if (d > 0)
                {
                    if (day == d + 1) {
                        day = -d;
                        break;
                    }

                    if (day > d + 1) {
                        day--;
                    }
                }
            }
            return new RabByungDay(m, day);
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return (IsLeap ? "闰" : "") + Names[Day - 1];
        }

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return RabByungMonth + GetName();
        }

        /// <summary>
        /// 转公历日
        /// </summary>
        /// <returns>公历日</returns>
        public SolarDay GetSolarDay()
        {
            var m = RabByungMonth.FromYm(1950, 12);
            var n = 0;
            while (!RabByungMonth.Equals(m)) {
                n += m.DayCount;
                m = m.Next(1);
            }
            var t = Day;
            foreach (var d in m.SpecialDays) {
                if (d < 0) {
                    if (t > -d) {
                        t--;
                    }
                } else if (d > 0) {
                    if (t > d) {
                        t++;
                    }
                }
            }
            if (IsLeap) {
                t++;
            }
            return SolarDay.FromYmd(1951, 1, 7).Next(n + t);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移天数</param>
        /// <returns>推移后的藏历日</returns>
        public new RabByungDay Next(int n)
        {
            return GetSolarDay().Next(n).GetRabByungDay();
        }
    }
}