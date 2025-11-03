using System;
using System.Collections.Generic;
using tyme.solar;

namespace tyme.sixtycycle
{
    /// <summary>
    /// 三柱
    /// </summary>
    public class ThreePillars : AbstractCulture
    {
        /// <summary>
        /// 年柱
        /// </summary>
        public SixtyCycle Year { get; }

        /// <summary>
        /// 月柱
        /// </summary>
        public SixtyCycle Month { get; }

        /// <summary>
        /// 日柱
        /// </summary>
        public SixtyCycle Day { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年柱</param>
        /// <param name="month">月柱</param>
        /// <param name="day">日柱</param>
        public ThreePillars(SixtyCycle year, SixtyCycle month, SixtyCycle day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年干支</param>
        /// <param name="month">月干支</param>
        /// <param name="day">日干支</param>
        public ThreePillars(string year, string month, string day) : this(SixtyCycle.FromName(year), SixtyCycle.FromName(month), SixtyCycle.FromName(day))
        {
        }
        
        /// <summary>
        /// 公历日列表
        /// </summary>
        /// <param name="startYear">开始年(含)，支持1-9999年</param>
        /// <param name="endYear">开始年(含)，支持1-9999年</param>
        /// <returns>公历日列表</returns>
        public List<SolarDay> GetSolarDays(int startYear, int endYear)
        {
            var l = new List<SolarDay>();
            // 月地支距寅月的偏移值
            var m = Month.EarthBranch.Next(-2).Index;
            // 月天干要一致
            if (!HeavenStem.FromIndex((Year.HeavenStem.Index + 1) * 2 + m).Equals(Month.HeavenStem))
            {
                return l;
            }

            // 1年的立春是辛酉，序号57
            var y = Year.Next(-57).Index + 1;
            // 节令偏移值
            m *= 2;
            var baseYear = startYear - 1;
            if (baseYear > y)
            {
                y += 60 * (int)Math.Ceiling((baseYear - y) / 60D);
            }

            while (y <= endYear)
            {
                // 立春为寅月的开始
                var term = SolarTerm.FromIndex(y, 3);
                // 节令推移，年干支和月干支就都匹配上了
                if (m > 0)
                {
                    term = term.Next(m);
                }

                var solarDay = term.JulianDay.GetSolarDay();
                if (solarDay.Year >= startYear)
                {
                    // 日干支和节令干支的偏移值
                    var d = Day.Next(-solarDay.GetLunarDay().SixtyCycle.Index).Index;
                    if (d > 0)
                    {
                        // 从节令推移天数
                        solarDay = solarDay.Next(d);
                    }

                    // 验证一下
                    if (solarDay.GetSixtyCycleDay().ThreePillars.Equals(this))
                    {
                        l.Add(solarDay);
                    }
                }

                y += 60;
            }

            return l;
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return $"{Year} {Month} {Day}";
        }
    }
}