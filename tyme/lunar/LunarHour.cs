using System;
using System.Collections.Generic;
using tyme.culture;
using tyme.culture.star.nine;
using tyme.culture.star.twelve;
using tyme.eightchar;
using tyme.sixtycycle;
using tyme.solar;

namespace tyme.lunar
{
    /// <summary>
    /// 农历时辰
    /// </summary>
    public class LunarHour : AbstractTyme
    {
        /// <summary>
        /// 农历日
        /// </summary>
        public LunarDay LunarDay { get; }

        /// <summary>
        /// 年
        /// </summary>
        public int Year => LunarDay.Year;

        /// <summary>
        /// 月，闰月为负
        /// </summary>
        public int Month => LunarDay.Month;

        /// <summary>
        /// 日
        /// </summary>
        public int Day => LunarDay.Day;

        /// <summary>
        /// 时
        /// </summary>
        public int Hour { get; }

        /// <summary>
        /// 分
        /// </summary>
        public int Minute { get; }

        /// <summary>
        /// 秒
        /// </summary>
        public int Second { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">农历年</param>
        /// <param name="month">农历月，闰月为负</param>
        /// <param name="day">农历日</param>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <exception cref="ArgumentException"></exception>
        public LunarHour(int year, int month, int day, int hour, int minute, int second)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentException($"illegal hour: {hour}");
            }

            if (minute < 0 || minute > 59)
            {
                throw new ArgumentException($"illegal minute: {minute}");
            }

            if (second < 0 || second > 59)
            {
                throw new ArgumentException($"illegal second: {second}");
            }

            LunarDay = LunarDay.FromYmd(year, month, day);
            Hour = hour;
            Minute = minute;
            Second = second;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">农历年</param>
        /// <param name="month">农历月</param>
        /// <param name="day">农历日</param>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <returns></returns>
        public static LunarHour FromYmdHms(int year, int month, int day, int hour, int minute, int second)
        {
            return new LunarHour(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return EarthBranch.FromIndex(IndexInDay).GetName() + "时";
        }

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return LunarDay + SixtyCycle.GetName() + "时";
        }

        /// <summary>
        /// 位于当天的索引
        /// </summary>
        public int IndexInDay => (Hour + 1) / 2;

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的农历时辰</returns>
        public new LunarHour Next(int n)
        {
            var h = Hour + n * 2;
            var diff = h < 0 ? -1 : 1;
            var hour = Math.Abs(h);
            var days = hour / 24 * diff;
            hour = (hour % 24) * diff;
            if (hour < 0)
            {
                hour += 24;
                days--;
            }

            var d = LunarDay.Next(days);
            return FromYmdHms(d.Year, d.Month, d.Day, hour, Minute, Second);
        }

        /// <summary>
        /// 是否在指定时辰之前
        /// </summary>
        /// <param name="target">时辰</param>
        /// <returns>True/False</returns>
        public bool IsBefore(LunarHour target)
        {
            if (!LunarDay.Equals(target.LunarDay))
            {
                return LunarDay.IsBefore(target.LunarDay);
            }

            return Hour != target.Hour ? Hour < target.Hour :
                Minute == target.Minute ? Second < target.Second : Minute < target.Minute;
        }

        /// <summary>
        /// 是否在指定时辰之后
        /// </summary>
        /// <param name="target">时辰</param>
        /// <returns>True/False</returns>
        public bool IsAfter(LunarHour target)
        {
            if (!LunarDay.Equals(target.LunarDay))
            {
                return LunarDay.IsAfter(target.LunarDay);
            }

            return Hour != target.Hour ? Hour > target.Hour :
                Minute == target.Minute ? Second > target.Second : Minute > target.Minute;
        }

        /// <summary>
        /// 当时的年干支（立春换）
        /// </summary>
        public SixtyCycle YearSixtyCycle
        {
            get
            {
                var solarTime = GetSolarTime();
                var solarYear = LunarDay.GetSolarDay().Year;
                var springSolarTime = SolarTerm.FromIndex(solarYear, 3).JulianDay.GetSolarTime();
                var lunarYear = LunarDay.LunarMonth.LunarYear;
                var year = lunarYear.Year;
                var sixtyCycle = lunarYear.SixtyCycle;
                if (year == solarYear)
                {
                    if (solarTime.IsBefore(springSolarTime))
                    {
                        sixtyCycle = sixtyCycle.Next(-1);
                    }
                }
                else if (year < solarYear)
                {
                    if (!solarTime.IsBefore(springSolarTime))
                    {
                        sixtyCycle = sixtyCycle.Next(1);
                    }
                }

                return sixtyCycle;
            }
        }

        /// <summary>
        /// 当时的月干支（节气换）
        /// </summary>
        public SixtyCycle MonthSixtyCycle
        {
            get
            {
                var solarTime = GetSolarTime();
                var year = solarTime.Year;
                var term = solarTime.Term;
                var index = term.Index - 3;
                if (index < 0 && term.JulianDay.GetSolarTime()
                        .IsAfter(SolarTerm.FromIndex(year, 3).JulianDay.GetSolarTime()))
                {
                    index += 24;
                }

                return LunarMonth.FromYm(year, 1).SixtyCycle.Next((int)Math.Floor(index * 1D / 2));
            }
        }

        /// <summary>
        /// 当时的日干支（23:00开始算做第二天）
        /// </summary>
        public SixtyCycle DaySixtyCycle
        {
            get
            {
                var d = LunarDay.SixtyCycle;
                return Hour < 23 ? d : d.Next(1);
            }
        }

        /// <summary>
        /// 干支
        /// </summary>
        public SixtyCycle SixtyCycle
        {
            get
            {
                var earthBranchIndex = IndexInDay % 12;
                var heavenStemIndex = DaySixtyCycle.HeavenStem.Index % 5 * 2 + earthBranchIndex;
                return SixtyCycle.FromName(HeavenStem.FromIndex(heavenStemIndex).GetName() +
                                           EarthBranch.FromIndex(earthBranchIndex).GetName());
            }
        }

        /// <summary>
        /// 黄道黑道十二神
        /// </summary>
        public TwelveStar TwelveStar =>
            TwelveStar.FromIndex(SixtyCycle.EarthBranch.Index + (8 - DaySixtyCycle.EarthBranch.Index % 6) * 2);

        /// <summary>
        /// 九星
        /// </summary>
        public NineStar NineStar
        {
            get
            {
                var solar = LunarDay.GetSolarDay();
                var dongZhi = SolarTerm.FromIndex(solar.Year, 0);
                var xiaZhi = dongZhi.Next(12);
                var asc = !solar.IsBefore(dongZhi.JulianDay.GetSolarDay()) &&
                          solar.IsBefore(xiaZhi.JulianDay.GetSolarDay());
                var start = new[] { 8, 5, 2 }[LunarDay.SixtyCycle.EarthBranch.Index % 3];
                if (asc)
                {
                    start = 8 - start;
                }

                var earthBranchIndex = IndexInDay % 12;
                return NineStar.FromIndex(start + (asc ? earthBranchIndex : -earthBranchIndex));
            }
        }

        /// <summary>
        /// 公历时刻
        /// </summary>
        /// <returns>公历时刻</returns>
        public SolarTime GetSolarTime()
        {
            var d = LunarDay.GetSolarDay();
            return SolarTime.FromYmdHms(d.Year, d.Month, d.Day, Hour, Minute, Second);
        }

        /// <summary>
        /// 八字
        /// </summary>
        public EightChar EightChar => new EightChar(YearSixtyCycle, MonthSixtyCycle, DaySixtyCycle, SixtyCycle);

        /// <summary>
        /// 宜
        /// </summary>
        /// <returns>宜忌列表</returns>
        public List<Taboo> Recommends => Taboo.GetHourRecommends(DaySixtyCycle, SixtyCycle);

        /// <summary>
        /// 忌
        /// </summary>
        /// <returns>宜忌列表</returns>
        public List<Taboo> Avoids => Taboo.GetHourAvoids(DaySixtyCycle, SixtyCycle);
    }
}