using System;
using System.Collections.Generic;
using tyme.culture;
using tyme.culture.fetus;
using tyme.culture.star.nine;
using tyme.jd;
using tyme.sixtycycle;
using tyme.solar;
using tyme.util;

namespace tyme.lunar
{
    /// <summary>
    /// 农历月
    /// </summary>
    public class LunarMonth : AbstractTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names = { "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月" };

        /// <summary>
        /// 农历年
        /// </summary>
        public LunarYear Year { get; }

        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; }
        
        /// <summary>
        /// 是否闰月
        /// </summary>
        public bool IsLeap { get; }

        /// <summary>
        /// 天数(大月30天，小月29天)
        /// </summary>
        public int DayCount { get; }

        /// <summary>
        /// 位于当年的索引，0-12
        /// </summary>
        public int IndexInYear { get; }

        /// <summary>
        /// 初一的儒略日
        /// </summary>
        public JulianDay FirstJulianDay { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">农历年</param>
        /// <param name="month">农历月</param>
        /// <exception cref="ArgumentException"></exception>
        public LunarMonth(int year, int month) {
            var currentYear = LunarYear.FromYear(year);
            var currentLeapMonth = currentYear.LeapMonth;
            if (month == 0 || month > 12 || month < -12) {
                throw new ArgumentException($"illegal lunar month: {month}");
            }
            var leap = month < 0;
            var m = Math.Abs(month);
            if (leap && m != currentLeapMonth) {
                throw new ArgumentException($"illegal leap month {m} in lunar year {year}");
            }

            // 冬至
            var dongZhi = SolarTerm.FromIndex(year, 0);

            // 冬至前的初一，今年首朔的日月黄经差
            var w = ShouXingUtil.CalcShuo(dongZhi.CursoryJulianDay);
            if (w > dongZhi.CursoryJulianDay) {
                w -= 29.53;
            }

            // 正常情况正月初一为第3个朔日，但有些特殊的
            var offset = 2;
            if (year > 8 && year < 24) {
                offset = 1;
            } else if (LunarYear.FromYear(year - 1).LeapMonth > 10 && year != 239 && year != 240) {
                offset = 3;
            }

            // 位于当年的索引
            var index = m - 1;
            if (leap || (currentLeapMonth > 0 && m > currentLeapMonth)) {
                index += 1;
            }
            IndexInYear = index;

            // 本月初一
            w += 29.5306 * (offset + index);
            var firstDay = ShouXingUtil.CalcShuo(w);
            FirstJulianDay = JulianDay.FromJulianDay(JulianDay.J2000 + firstDay);
            // 本月天数 = 下月初一 - 本月初一
            DayCount = (int) (ShouXingUtil.CalcShuo(w + 29.5306) - firstDay);
            Year = currentYear;
            Month = m;
            IsLeap = leap;
        }

        /// <summary>
        /// 从农历年月初始化
        /// </summary>
        /// <param name="year">农历年</param>
        /// <param name="month">农历月，闰月为负</param>
        /// <returns>农历月</returns>
        public static LunarMonth FromYm(int year, int month)
        {
            return new LunarMonth(year, month);
        }
        
        /// <summary>
        /// 月，当月为闰月时，返回负数
        /// </summary>
        public int MonthWithLeap => IsLeap ? -Month : Month;

        /// <summary>
        /// 名称，依据国家标准《农历的编算和颁行》GB/T 33661-2017中农历月的命名方法。
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return (IsLeap ? "闰" : "") + Names[Month - 1];
        }

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return Year + GetName();
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的农历月</returns>
        public new LunarMonth Next(int n)
        {
            if (n == 0) {
                return FromYm(Year.Year, MonthWithLeap);
            }
            var m = IndexInYear + 1 + n;
            var y = Year;
            var leapMonth = y.LeapMonth;
            var monthSize = 12 + (leapMonth > 0 ? 1 : 0);
            var forward = n > 0;
            var add = forward ? 1 : -1;
            while (forward ? (m > monthSize) : (m <= 0)) {
                if (forward) {
                    m -= monthSize;
                }
                y = y.Next(add);
                leapMonth = y.LeapMonth;
                monthSize = 12 + (leapMonth > 0 ? 1 : 0);
                if (!forward) {
                    m += monthSize;
                }
            }
            var leap = false;
            if (leapMonth > 0) {
                if (m == leapMonth + 1) {
                    leap = true;
                }
                if (m > leapMonth) {
                    m--;
                }
            }
            return FromYm(y.Year, leap ? -m : m);
        }

        /// <summary>
        /// 农历季节
        /// </summary>
        public LunarSeason Season => LunarSeason.FromIndex(Month - 1);

        /// <summary>
        /// 周数
        /// </summary>
        /// <param name="start">起始星期，1234560分别代表星期一至星期天</param>
        /// <returns>周数</returns>
        public int GetWeekCount(int start)
        {
            return (int) Math.Ceiling((IndexOf(FirstJulianDay.Week.Index - start, 7) + DayCount) / 7D);
        }

        /// <summary>
        /// 获取本月的农历周列表
        /// </summary>
        /// <param name="start">星期几作为一周的开始，1234560分别代表星期一至星期天</param>
        /// <returns>公历周列表</returns>
        public List<LunarWeek> GetWeeks(int start)
        {
            var size = GetWeekCount(start);
            var l = new List<LunarWeek>(size);
            for (var i = 0; i < size; i++)
            {
                l.Add(LunarWeek.FromYm(Year.Year, MonthWithLeap, i, start));
            }

            return l;
        }

        /// <summary>
        /// 本月的农历日列表
        /// </summary>
        public List<LunarDay> Days
        {
            get
            {
                var l = new List<LunarDay>(DayCount);
                for (var i = 0; i < DayCount; i++)
                {
                    l.Add(LunarDay.FromYmd(Year.Year, MonthWithLeap, i + 1));
                }

                return l;
            }
        }
        
        /// <summary>
        /// 干支
        /// </summary>
        public SixtyCycle SixtyCycle => SixtyCycle.FromName(HeavenStem.FromIndex((Year.SixtyCycle.HeavenStem.Index + 1) * 2 + IndexInYear).GetName() + EarthBranch.FromIndex(IndexInYear + 2).GetName());
        
        /// <summary>
        /// 九星
        /// </summary>
        public NineStar NineStar => NineStar.FromIndex(27 - Year.SixtyCycle.EarthBranch.Index % 3 * 3 - SixtyCycle.EarthBranch.Index);

        /// <summary>
        /// 太岁方位
        /// </summary>
        public Direction JupiterDirection
        {
            get
            {
                var n = new []{7, -1, 1, 3}[SixtyCycle.EarthBranch.Next(-2).Index % 4];
                return n == -1 ? SixtyCycle.HeavenStem.Direction : Direction.FromIndex(n);
            }
        }
        
        /// <summary>
        /// 逐月胎神
        /// </summary>
        public FetusMonth Fetus => FetusMonth.FromLunarMonth(this);

        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="o">其他对象</param>
        /// <returns>True/False</returns>
        public override bool Equals(object o)
        {
            return o is LunarMonth target && Year.Equals(target.Year) && MonthWithLeap == target.MonthWithLeap;
        }

        /// <summary>
        /// HashCode
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}