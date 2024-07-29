using System;
using System.Collections.Generic;

namespace tyme.solar
{
    /// <summary>
    /// 公历月
    /// </summary>
    public class SolarMonth : AbstractTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names = { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月" };

        /// <summary>
        /// 每月天数
        /// </summary>
        public static readonly int[] DayCounts = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        /// <summary>
        /// 公历年
        /// </summary>
        public SolarYear SolarYear { get; }

        /// <summary>
        /// 年
        /// </summary>
        public int Year => SolarYear.Year;

        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <exception cref="ArgumentException"></exception>
        public SolarMonth(int year, int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentException($"illegal solar month: {month}");
            }

            SolarYear = SolarYear.FromYear(year);
            Month = month;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="month">公历月</param>
        /// <returns>公历月</returns>
        public static SolarMonth FromYm(int year, int month)
        {
            return new SolarMonth(year, month);
        }

        /// <summary>
        /// 天数（1582年10月只有21天)
        /// </summary>
        public int DayCount
        {
            get
            {
                if (1582 == Year && 10 == Month)
                {
                    return 21;
                }

                var d = DayCounts[IndexInYear];
                //公历闰年2月多一天
                if (2 == Month && SolarYear.IsLeap)
                {
                    d++;
                }

                return d;
            }
        }

        /// <summary>
        /// 位于当年的索引(0-11)
        /// </summary>
        public int IndexInYear => Month - 1;

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return Names[IndexInYear];
        }

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return SolarYear + GetName();
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的公历月</returns>
        public new SolarMonth Next(int n)
        {
            if (n == 0)
            {
                return FromYm(Year, Month);
            }

            var m = Month + n;
            var y = Year + m / 12;
            m %= 12;
            if (m < 1)
            {
                m += 12;
                y--;
            }

            return FromYm(y, m);
        }

        /// <summary>
        /// 公历季度
        /// </summary>
        public SolarSeason Season => SolarSeason.FromIndex(Year, IndexInYear / 3);

        /// <summary>
        /// 周数
        /// </summary>
        /// <param name="start">起始星期，1234560分别代表星期一至星期天</param>
        /// <returns>周数</returns>
        public int GetWeekCount(int start)
        {
            return (int)Math.Ceiling((IndexOf(SolarDay.FromYmd(Year, Month, 1).Week.Index - start, 7) + DayCount) / 7D);
        }

        /// <summary>
        /// 获取本月的公历周列表
        /// </summary>
        /// <param name="start">星期几作为一周的开始，1234560分别代表星期一至星期天</param>
        /// <returns>公历周列表</returns>
        public List<SolarWeek> GetWeeks(int start)
        {
            var size = GetWeekCount(start);
            var l = new List<SolarWeek>(size);
            for (var i = 0; i < size; i++)
            {
                l.Add(SolarWeek.FromYm(Year, Month, i, start));
            }

            return l;
        }

        /// <summary>
        /// 本月的公历日列表
        /// </summary>
        public List<SolarDay> Days
        {
            get
            {
                var l = new List<SolarDay>(DayCount);
                for (var i = 1; i <= DayCount; i++)
                {
                    l.Add(SolarDay.FromYmd(Year, Month, i));
                }

                return l;
            }
        }
    }
}