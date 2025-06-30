using System;
using System.Collections.Generic;
using tyme.culture;
using tyme.sixtycycle;
using tyme.solar;

namespace tyme.eightchar
{
    /// <summary>
    /// 八字
    /// </summary>
    public class EightChar : AbstractCulture
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
        /// 时柱
        /// </summary>
        public SixtyCycle Hour { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年柱</param>
        /// <param name="month">月柱</param>
        /// <param name="day">日柱</param>
        /// <param name="hour">时柱</param>
        public EightChar(SixtyCycle year, SixtyCycle month, SixtyCycle day, SixtyCycle hour)
        {
            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年干支</param>
        /// <param name="month">月干支</param>
        /// <param name="day">日干支</param>
        /// <param name="hour">时干支</param>
        public EightChar(string year, string month, string day, string hour) : this(SixtyCycle.FromName(year), SixtyCycle.FromName(month), SixtyCycle.FromName(day), SixtyCycle.FromName(hour))
        {
        }

        /// <summary>
        /// 胎元
        /// </summary>
        public SixtyCycle FetalOrigin => SixtyCycle.FromName(Month.HeavenStem.Next(1).GetName() + Month.EarthBranch.Next(3).GetName());

        /// <summary>
        /// 胎息
        /// </summary>
        public SixtyCycle FetalBreath => SixtyCycle.FromName(Day.HeavenStem.Next(5).GetName() + EarthBranch.FromIndex(13 - Day.EarthBranch.Index).GetName());

        /// <summary>
        /// 命宫
        /// </summary>
        public SixtyCycle OwnSign
        {
            get
            {
                var m = Month.EarthBranch.Index - 1;
                if (m < 1)
                {
                    m += 12;
                }
                var h = Hour.EarthBranch.Index - 1;
                if (h < 1)
                {
                    h += 12;
                }
                var offset = m + h;
                offset = (offset >= 14 ? 26 : 14) - offset;
                return SixtyCycle.FromName(HeavenStem.FromIndex((Year.HeavenStem.Index + 1) * 2 + offset - 1).GetName() + EarthBranch.FromIndex(offset + 1).GetName());
            }
        }

        /// <summary>
        /// 身宫
        /// </summary>
        public SixtyCycle BodySign
        {
            get
            {
                var offset = Month.EarthBranch.Index - 1;
                if (offset < 1)
                {
                    offset += 12;
                }
                offset += Hour.EarthBranch.Index + 1;
                if (offset > 12)
                {
                    offset -= 12;
                }
                return SixtyCycle.FromName(HeavenStem.FromIndex((Year.HeavenStem.Index + 1) * 2 + offset - 1).GetName() + EarthBranch.FromIndex(offset + 1).GetName());
            }
        }

        /// <summary>
        /// 建除十二值神
        /// </summary>
        [Obsolete("该方法已过时，请使用SixtyCycleDay")]
        public Duty Duty => Duty.FromIndex(Day.EarthBranch.Index - Month.EarthBranch.Index);

        /// <summary>
        /// 公历时刻列表
        /// </summary>
        /// <param name="startYear">开始年(含)，支持1-9999年</param>
        /// <param name="endYear">开始年(含)，支持1-9999年</param>
        /// <returns>公历时刻列表</returns>
        public List<SolarTime> GetSolarTimes(int startYear, int endYear)
        {
            var l = new List<SolarTime>();
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
            // 时辰地支转时刻
            var h = Hour.EarthBranch.Index * 2;
            // 兼容子时多流派
            var hours = h == 0 ? new[] { 0, 23 } : new[] { h };
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

                var solarTime = term.JulianDay.GetSolarTime();
                if (solarTime.Year >= startYear)
                {
                    // 日干支和节令干支的偏移值
                    var solarDay = solarTime.SolarDay;
                    var d = Day.Next(-solarDay.GetLunarDay().SixtyCycle.Index).Index;
                    if (d > 0)
                    {
                        // 从节令推移天数
                        solarDay = solarDay.Next(d);
                    }

                    foreach (var hour in hours)
                    {
                        var mi = 0;
                        var s = 0;
                        if (d == 0 && h == solarTime.Hour)
                        {
                            // 如果正好是节令当天，且小时和节令的小时数相等的极端情况，把分钟和秒钟带上
                            mi = solarTime.Minute;
                            s = solarTime.Second;
                        }

                        var time = SolarTime.FromYmdHms(solarDay.Year, solarDay.Month, solarDay.Day, hour, mi, s);
                        // 验证一下
                        if (time.GetLunarHour().EightChar.Equals(this))
                        {
                            l.Add(time);
                        }
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
            return $"{Year} {Month} {Day} {Hour}";
        }
    }
}