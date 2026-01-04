using System;
using System.Collections.Generic;
using tyme.culture;
using tyme.culture.fetus;
using tyme.culture.ren;
using tyme.culture.star.nine;
using tyme.jd;
using tyme.sixtycycle;
using tyme.solar;
using tyme.unit;
using tyme.util;

namespace tyme.lunar
{
    /// <summary>
    /// 农历月
    /// </summary>
    public class LunarMonth : MonthUnit
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names = { "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月" };

        /// <summary>
        /// 农历年
        /// </summary>
        public LunarYear LunarYear => LunarYear.FromYear(Year);

        /// <summary>
        /// 是否闰月
        /// </summary>
        public bool IsLeap { get; }
        
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="year">农历年</param>
        /// <param name="month">农历月，闰月为负</param>
        /// <exception cref="ArgumentException">参数异常</exception>
        public static void Validate(int year, int month)
        {
            if (month == 0 || month > 12 || month < -12)
            {
                throw new ArgumentException($"illegal lunar month: {month}");
            }
            if (month < 0 && -month != LunarYear.FromYear(year).LeapMonth)
            {
                throw new ArgumentException($"illegal leap month {-month} in lunar year {year}");
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">农历年</param>
        /// <param name="month">农历月，闰月为负</param>
        /// <exception cref="ArgumentException"></exception>
        public LunarMonth(int year, int month)
        {
            Validate(year, month);
            Year = year;
            Month = Math.Abs(month);
            IsLeap = month < 0;
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
        /// 初一的儒略日
        /// </summary>
        /// <returns>初一的儒略日</returns>
        protected double GetNewMoon() {
            // 冬至
            var dongZhiJd = SolarTerm.FromIndex(Year, 0).CursoryJulianDay;

            // 冬至前的初一，今年首朔的日月黄经差
            var w = ShouXingUtil.CalcShuo(dongZhiJd);
            if (w > dongZhiJd) {
                w -= 29.53;
            }

            // 正常情况正月初一为第3个朔日，但有些特殊的
            var offset = 2;
            if (Year > 8 && Year < 24) {
                offset = 1;
            } else if (LunarYear.FromYear(Year - 1).LeapMonth > 10 && Year != 239 && Year != 240) {
                offset = 3;
            }

            // 本月初一
            return w + 29.5306 * (offset + IndexInYear);
        }

        /// <summary>
        /// 位于当年的索引(0-12)
        /// </summary>
        public int IndexInYear
        {
            get
            {
                var index = Month - 1;
                if (IsLeap) {
                    index += 1;
                } else {
                    var leapMonth = LunarYear.FromYear(Year).LeapMonth;
                    if (leapMonth > 0 && Month > leapMonth) {
                        index += 1;
                    }
                }
                return index;
            }
        }

        /// <summary>
        /// 天数(大月30天，小月29天)
        /// </summary>
        public int DayCount
        {
            get
            {
                var w = GetNewMoon();
                // 本月天数 = 下月初一 - 本月初一
                return (int) (ShouXingUtil.CalcShuo(w + 29.5306) - ShouXingUtil.CalcShuo(w));
            }
        }
        
        /// <summary>
        /// 初一的儒略日
        /// </summary>
        public JulianDay FirstJulianDay => JulianDay.FromJulianDay(JulianDay.J2000 + ShouXingUtil.CalcShuo(GetNewMoon()));

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
            return LunarYear + GetName();
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的农历月</returns>
        public new LunarMonth Next(int n)
        {
            if (n == 0)
            {
                return FromYm(Year, MonthWithLeap);
            }

            var m = IndexInYear + 1 + n;
            var y = LunarYear;
            if (n > 0)
            {
                while (m > y.MonthCount)
                {
                    m -= y.MonthCount;
                    y = y.Next(1);
                }
            }
            else
            {
                while (m <= 0)
                {
                    y = y.Next(-1);
                    m += y.MonthCount;
                }
            }

            var leap = false;
            var leapMonth = y.LeapMonth;
            if (leapMonth > 0)
            {
                if (m == leapMonth + 1)
                {
                    leap = true;
                }

                if (m > leapMonth)
                {
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
            return (int)Math.Ceiling((IndexOf(FirstJulianDay.Week.Index - start, 7) + DayCount) / 7D);
        }

        /// <summary>
        /// 本月的农历周列表
        /// </summary>
        /// <param name="start">星期几作为一周的开始，1234560分别代表星期一至星期天</param>
        /// <returns>公历周列表</returns>
        public List<LunarWeek> GetWeeks(int start)
        {
            var size = GetWeekCount(start);
            var l = new List<LunarWeek>(size);
            for (var i = 0; i < size; i++)
            {
                l.Add(LunarWeek.FromYm(Year, MonthWithLeap, i, start));
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
                for (var i = 1; i <= DayCount; i++)
                {
                    l.Add(LunarDay.FromYmd(Year, MonthWithLeap, i));
                }

                return l;
            }
        }
        
        /// <summary>
        /// 初一
        /// </summary>
        public LunarDay FirstDay => LunarDay.FromYmd(Year, MonthWithLeap, 1);

        /// <summary>
        /// 干支
        /// </summary>
        public SixtyCycle SixtyCycle => SixtyCycle.FromName(HeavenStem.FromIndex(LunarYear.SixtyCycle.HeavenStem.Index * 2 + Month + 1).GetName() + EarthBranch.FromIndex(Month + 1).GetName());

        /// <summary>
        /// 九星
        /// </summary>
        public NineStar NineStar
        {
            get
            {
                var index = SixtyCycle.EarthBranch.Index;
                if (index < 2)
                {
                    index += 3;
                }

                return NineStar.FromIndex(27 - LunarYear.SixtyCycle.EarthBranch.Index % 3 * 3 - index);
            }
        }

        /// <summary>
        /// 太岁方位
        /// </summary>
        public Direction JupiterDirection
        {
            get
            {
                var n = new[] { 7, -1, 1, 3 }[SixtyCycle.EarthBranch.Next(-2).Index % 4];
                return n != -1 ? Direction.FromIndex(n) : SixtyCycle.HeavenStem.Direction;
            }
        }

        /// <summary>
        /// 逐月胎神
        /// </summary>
        public FetusMonth Fetus => FetusMonth.FromLunarMonth(this);

        /// <summary>
        /// 小六壬
        /// </summary>
        public MinorRen MinorRen => MinorRen.FromIndex((Month - 1) % 6);
    }
}