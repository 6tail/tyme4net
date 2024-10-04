using tyme.solar;

namespace tyme.eightchar.provider.impl
{
    /// <summary>
    /// 童限计算抽象
    /// </summary>
    public abstract class AbstractChildLimitProvider : IChildLimitProvider
    {
        /// <summary>
        /// 计算童限信息
        /// </summary>
        /// <param name="birthTime">出生公历时刻</param>
        /// <param name="addYear">追加年数</param>
        /// <param name="addMonth">追加月数</param>
        /// <param name="addDay">追加日数</param>
        /// <param name="addHour">追加小时数</param>
        /// <param name="addMinute">追加分钟数</param>
        /// <param name="addSecond">追加秒数</param>
        /// <returns>童限信息</returns>
        protected ChildLimitInfo Next(SolarTime birthTime, int addYear, int addMonth, int addDay, int addHour,
            int addMinute, int addSecond)
        {
            var d = birthTime.Day + addDay;
            var h = birthTime.Hour + addHour;
            var mi = birthTime.Minute + addMinute;
            var s = birthTime.Second + addSecond;
            mi += s / 60;
            s %= 60;
            h += mi / 60;
            mi %= 60;
            d += h / 24;
            h %= 24;

            var sm = SolarMonth.FromYm(birthTime.Year + addYear, birthTime.Month).Next(addMonth);

            var dc = sm.DayCount;
            while (d > dc)
            {
                d -= dc;
                sm = sm.Next(1);
                dc = sm.DayCount;
            }

            return new ChildLimitInfo(birthTime, SolarTime.FromYmdHms(sm.Year, sm.Month, d, h, mi, s), addYear,
                addMonth, addDay, addHour, addMinute);
        }

        /// <inheritdoc />
        public abstract ChildLimitInfo GetInfo(SolarTime birthTime, SolarTerm term);
    }
}