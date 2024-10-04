using tyme.solar;

namespace tyme.eightchar.provider.impl
{
    /// <summary>
    /// Lunar的流派1童限计算（按天数和时辰数计算，3天1年，1天4个月，1时辰10天）
    /// </summary>
    public class LunarSect1ChildLimitProvider : AbstractChildLimitProvider
    {
        /// <inheritdoc />
        public override ChildLimitInfo GetInfo(SolarTime birthTime, SolarTerm term)
        {
            var termTime = term.JulianDay.GetSolarTime();
            var end = termTime;
            var start = birthTime;
            if (birthTime.IsAfter(termTime))
            {
                end = birthTime;
                start = termTime;
            }

            var endTimeZhiIndex = (end.Hour == 23) ? 11 : end.GetLunarHour().IndexInDay;
            var startTimeZhiIndex = (start.Hour == 23) ? 11 : start.GetLunarHour().IndexInDay;
            // 时辰差
            var hourDiff = endTimeZhiIndex - startTimeZhiIndex;
            // 天数差
            var dayDiff = end.SolarDay.Subtract(start.SolarDay);
            if (hourDiff < 0)
            {
                hourDiff += 12;
                dayDiff--;
            }

            var monthDiff = hourDiff * 10 / 30;
            var month = dayDiff * 4 + monthDiff;
            var day = hourDiff * 10 - monthDiff * 30;
            var year = month / 12;
            month = month - year * 12;

            return Next(birthTime, year, month, day, 0, 0, 0);
        }
    }
}