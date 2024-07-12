using System;
using tyme.solar;

namespace tyme.eightchar.provider.impl
{
    /// <summary>
    /// 默认的童限计算
    /// </summary>
    public class DefaultChildLimitProvider : IChildLimitProvider
    {
        /// <summary>
        /// 童限信息
        /// </summary>
        /// <param name="birthTime">出生公历时刻</param>
        /// <param name="term">节令</param>
        /// <returns>童限信息</returns>
        public ChildLimitInfo GetInfo(SolarTime birthTime, SolarTerm term)
        {
            // 出生时刻和节令时刻相差的秒数
            var seconds = Math.Abs(term.JulianDay.GetSolarTime().Subtract(birthTime));
            // 3天 = 1年，3天=60*60*24*3秒=259200秒 = 1年
            var year = seconds / 259200;
            seconds %= 259200;
            // 1天 = 4月，1天=60*60*24秒=86400秒 = 4月，85400秒/4=21600秒 = 1月
            var month = seconds / 21600;
            seconds %= 21600;
            // 1时 = 5天，1时=60*60秒=3600秒 = 5天，3600秒/5=720秒 = 1天
            var day = seconds / 720;
            seconds %= 720;
            // 1分 = 2时，60秒 = 2时，60秒/2=30秒 = 1时
            var hour = seconds / 30;
            seconds %= 30;
            // 1秒 = 2分，1秒/2=0.5秒 = 1分
            var minute = seconds * 2;

            var d = birthTime.Day + day;
            var h = birthTime.Hour + hour;
            var mi = birthTime.Minute + minute;
            h += mi / 60;
            mi %= 60;
            d += h / 24;
            h %= 24;

            var sm = SolarMonth.FromYm(birthTime.Year + year, birthTime.Month).Next(month);

            var dc = sm.DayCount;
            if (d > dc)
            {
                d -= dc;
                sm = sm.Next(1);
            }

            return new ChildLimitInfo(birthTime, SolarTime.FromYmdHms(sm.Year, sm.Month, d, h, mi, birthTime.Second),
                year, month, day, hour, minute);
        }
    }
}