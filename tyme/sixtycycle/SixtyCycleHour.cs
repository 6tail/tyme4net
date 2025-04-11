using System;
using System.Collections.Generic;
using tyme.culture;
using tyme.culture.star.nine;
using tyme.culture.star.twelve;
using tyme.eightchar;
using tyme.lunar;
using tyme.solar;

namespace tyme.sixtycycle
{
    /// <summary>
    /// 干支时辰（立春换年，节令换月，23点换日）
    /// </summary>
    public class SixtyCycleHour : AbstractTyme
    {
        /// <summary>
        /// 公历时刻
        /// </summary>
        public SolarTime SolarTime { get; }

        /// <summary>
        /// 干支日
        /// </summary>
        public SixtyCycleDay SixtyCycleDay;

        /// <summary>
        /// 时柱
        /// </summary>
        public SixtyCycle Hour;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="solarTime">公历时刻</param>
        public SixtyCycleHour(SolarTime solarTime)
        {
            var solarYear = solarTime.Year;
            var springSolarTime = SolarTerm.FromIndex(solarYear, 3).JulianDay.GetSolarTime();
            var lunarHour = solarTime.GetLunarHour();
            var lunarDay = lunarHour.LunarDay;
            var lunarYear = lunarDay.LunarMonth.LunarYear;
            if (lunarYear.Year == solarYear)
            {
                if (solarTime.IsBefore(springSolarTime))
                {
                    lunarYear = lunarYear.Next(-1);
                }
            }
            else if (lunarYear.Year < solarYear)
            {
                if (!solarTime.IsBefore(springSolarTime))
                {
                    lunarYear = lunarYear.Next(1);
                }
            }

            var term = solarTime.Term;
            var index = term.Index - 3;
            if (index < 0 && term.JulianDay.GetSolarTime().IsAfter(SolarTerm.FromIndex(solarYear, 3).JulianDay.GetSolarTime()))
            {
                index += 24;
            }

            var d = lunarDay.SixtyCycle;
            SolarTime = solarTime;
            SixtyCycleDay = new SixtyCycleDay(solarTime.SolarDay, new SixtyCycleMonth(SixtyCycleYear.FromYear(lunarYear.Year), LunarMonth.FromYm(solarYear, 1).SixtyCycle.Next((int)Math.Floor(index * 1D / 2))), solarTime.Hour < 23 ? d : d.Next(1));
            Hour = lunarHour.SixtyCycle;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="solarTime">公历时刻</param>
        /// <returns>干支时辰</returns>
        public static SixtyCycleHour FromSolarTime(SolarTime solarTime)
        {
            return new SixtyCycleHour(solarTime);
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return $"{Hour}时";
        }

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return $"{Day}{GetName()}";
        }

        /// <summary>
        /// 位于当天的索引
        /// </summary>
        public int IndexInDay => SolarTime.Hour == 23 ? 0 : (SolarTime.Hour + 1) / 2;

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移秒数</param>
        /// <returns>推移后的干支时辰</returns>
        public new SixtyCycleHour Next(int n)
        {
            return FromSolarTime(SolarTime.Next(n));
        }

        /// <summary>
        /// 年柱
        /// </summary>
        public SixtyCycle Year => SixtyCycleDay.Year;

        /// <summary>
        /// 月柱
        /// </summary>
        public SixtyCycle Month => SixtyCycleDay.Month;

        /// <summary>
        /// 日柱
        /// </summary>
        public SixtyCycle Day => SixtyCycleDay.SixtyCycle;

        /// <summary>
        /// 干支
        /// </summary>
        public SixtyCycle SixtyCycle => Hour;

        /// <summary>
        /// 黄道黑道十二神
        /// </summary>
        public TwelveStar TwelveStar => TwelveStar.FromIndex(Hour.EarthBranch.Index + (8 - Day.EarthBranch.Index % 6) * 2);

        /// <summary>
        /// 九星
        /// </summary>
        public NineStar NineStar
        {
            get
            {
                var solar = SolarTime.SolarDay;
                var dongZhi = SolarTerm.FromIndex(solar.Year, 0);
                var xiaZhi = dongZhi.Next(12);
                var asc = !solar.IsBefore(dongZhi.JulianDay.GetSolarDay()) &&
                          solar.IsBefore(xiaZhi.JulianDay.GetSolarDay());
                var start = new[] { 8, 5, 2 }[Day.EarthBranch.Index % 3];
                if (asc)
                {
                    start = 8 - start;
                }

                var earthBranchIndex = IndexInDay % 12;
                return NineStar.FromIndex(start + (asc ? earthBranchIndex : -earthBranchIndex));
            }
        }

        /// <summary>
        /// 八字
        /// </summary>
        public EightChar EightChar => new EightChar(Year, Month, Day, Hour);

        /// <summary>
        /// 宜
        /// </summary>
        /// <returns>宜忌列表</returns>
        public List<Taboo> Recommends => Taboo.GetHourRecommends(Day, Hour);

        /// <summary>
        /// 忌
        /// </summary>
        /// <returns>宜忌列表</returns>
        public List<Taboo> Avoids => Taboo.GetHourAvoids(Day, Hour);
    }
}