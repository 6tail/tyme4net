using System;
using System.Collections.Generic;
using tyme.culture;
using tyme.culture.ren;
using tyme.culture.star.nine;
using tyme.culture.star.twelve;
using tyme.eightchar;
using tyme.eightchar.provider;
using tyme.eightchar.provider.impl;
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
        /// 八字计算接口
        /// </summary>
        public static IEightCharProvider Provider = new DefaultEightCharProvider();

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
        /// 公历时刻（第一次使用时才会初始化）
        /// </summary>
        protected SolarTime SolarTime;

        /// <summary>
        /// 干支时辰（第一次使用时才会初始化）
        /// </summary>
        protected SixtyCycleHour SixtyCycleHour;

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
            if (n == 0)
            {
                return FromYmdHms(Year, Month, Day, Hour, Minute, Second);
            }

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

            return Hour != target.Hour ? Hour < target.Hour : Minute == target.Minute ? Second < target.Second : Minute < target.Minute;
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

            return Hour != target.Hour ? Hour > target.Hour : Minute == target.Minute ? Second > target.Second : Minute > target.Minute;
        }

        /// <summary>
        /// 当时的年干支（立春换）
        /// </summary>
        [Obsolete("该方法已过时，请使用SixtyCycleHour")]
        public SixtyCycle YearSixtyCycle => GetSixtyCycleHour().Year;

        /// <summary>
        /// 当时的月干支（节气换）
        /// </summary>
        [Obsolete("该方法已过时，请使用SixtyCycleHour")]
        public SixtyCycle MonthSixtyCycle => GetSixtyCycleHour().Month;

        /// <summary>
        /// 当时的日干支（23:00开始算做第二天）
        /// </summary>
        [Obsolete("该方法已过时，请使用SixtyCycleHour")]
        public SixtyCycle DaySixtyCycle => GetSixtyCycleHour().Day;

        /// <summary>
        /// 干支
        /// </summary>
        public SixtyCycle SixtyCycle
        {
            get
            {
                var earthBranchIndex = IndexInDay % 12;
                var d = LunarDay.SixtyCycle;
                if (Hour >= 23) {
                    d = d.Next(1);
                }
                return SixtyCycle.FromName(HeavenStem.FromIndex(d.HeavenStem.Index % 5 * 2 + earthBranchIndex).GetName() + EarthBranch.FromIndex(earthBranchIndex).GetName());
            }
        }

        /// <summary>
        /// 黄道黑道十二神
        /// </summary>
        public TwelveStar TwelveStar => TwelveStar.FromIndex(SixtyCycle.EarthBranch.Index + (8 - GetSixtyCycleHour().Day.EarthBranch.Index % 6) * 2);

        /// <summary>
        /// 九星
        /// </summary>
        public NineStar NineStar
        {
            get
            {
                var solar = LunarDay.GetSolarDay();
                var dongZhi = SolarTerm.FromIndex(solar.Year, 0);
                var earthBranchIndex = IndexInDay % 12;
                var index = new[] { 8, 5, 2 }[LunarDay.SixtyCycle.EarthBranch.Index % 3];
                if (!solar.IsBefore(dongZhi.JulianDay.GetSolarDay()) && solar.IsBefore(dongZhi.Next(12).JulianDay.GetSolarDay()))
                {
                    index = 8 + earthBranchIndex - index;
                } else {
                    index -= earthBranchIndex;
                }
                return NineStar.FromIndex(index);
            }
        }

        /// <summary>
        /// 公历时刻
        /// </summary>
        /// <returns>公历时刻</returns>
        public SolarTime GetSolarTime()
        {
            if (null == SolarTime)
            {
                var d = LunarDay.GetSolarDay();
                SolarTime = SolarTime.FromYmdHms(d.Year, d.Month, d.Day, Hour, Minute, Second);
            }

            return SolarTime;
        }

        /// <summary>
        /// 八字
        /// </summary>
        public EightChar EightChar => Provider.GetEightChar(this);

        /// <summary>
        /// 宜
        /// </summary>
        /// <returns>宜忌列表</returns>
        public List<Taboo> Recommends => Taboo.GetHourRecommends(GetSixtyCycleHour().Day, SixtyCycle);

        /// <summary>
        /// 忌
        /// </summary>
        /// <returns>宜忌列表</returns>
        public List<Taboo> Avoids => Taboo.GetHourAvoids(GetSixtyCycleHour().Day, SixtyCycle);

        /// <summary>
        /// 小六壬
        /// </summary>
        public MinorRen MinorRen => LunarDay.MinorRen.Next(IndexInDay);

        /// <summary>
        /// 干支时辰
        /// </summary>
        /// <returns>干支时辰</returns>
        public SixtyCycleHour GetSixtyCycleHour()
        {
            return SixtyCycleHour ?? (SixtyCycleHour = GetSolarTime().GetSixtyCycleHour());
        }
    }
}