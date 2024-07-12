using System;
using tyme.solar;

namespace tyme.eightchar.provider.impl
{
    /// <summary>
    /// 元亨利贞的童限计算
    /// </summary>
    public class China95ChildLimitProvider : IChildLimitProvider
    {
        /// <summary>
        /// 童限信息
        /// </summary>
        /// <param name="birthTime">出生公历时刻</param>
        /// <param name="term">节令</param>
        /// <returns>童限信息</returns>
        public ChildLimitInfo GetInfo(SolarTime birthTime, SolarTerm term)
        {
            // 出生时刻和节令时刻相差的分钟数
            var minutes = Math.Abs(term.JulianDay.GetSolarTime().Subtract(birthTime)) / 60;
            var year = minutes / 4320;
            minutes %= 4320;
            var month = minutes / 360;
            minutes %= 360;
            var day = minutes / 12;

            var sm = SolarMonth.FromYm(birthTime.Year + year, birthTime.Month).Next(month);

            var d = birthTime.Day + day;
            var dc = sm.DayCount;
            if (d > dc) {
                d -= dc;
                sm = sm.Next(1);
            }

            return new ChildLimitInfo(birthTime, SolarTime.FromYmdHms(sm.Year, sm.Month, d, birthTime.Hour, birthTime.Minute, birthTime.Second), year, month, day, 0, 0);
        }
    }
}