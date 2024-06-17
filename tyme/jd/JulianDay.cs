using System;
using tyme.culture;
using tyme.solar;

namespace tyme.jd
{
    /// <summary>
    /// 儒略日
    /// </summary>
    public class JulianDay : AbstractTyme
    {
        /// <summary>
        /// 2000年儒略日数(2000-1-1 12:00:00 UTC)
        /// </summary>
        public const double J2000 = 2451545;

        /// <summary>
        /// 儒略日
        /// </summary>
        public double Day { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="day">儒略日</param>
        public JulianDay(double day)
        {
            Day = day;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="day">儒略日</param>
        /// <returns>儒略日</returns>
        public static JulianDay FromJulianDay(double day)
        {
            return new JulianDay(day);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="month">公历月</param>
        /// <param name="day">公历日</param>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <returns>儒略日</returns>
        public static JulianDay FromYmdHms(int year, int month, int day, int hour, int minute, int second)
        {
            var d = day + ((second * 1D / 60 + minute) / 60 + hour) / 24;
            var n = 0;
            var g = year * 372 + month * 31 + (int)d >= 588829;
            if (month <= 2)
            {
                month += 12;
                year--;
            }

            if (g)
            {
                n = (int)(year * 1D / 100);
                n = 2 - n + (int)(n * 1D / 4);
            }

            return FromJulianDay((int)(365.25 * (year + 4716)) + (int)(30.6001 * (month + 1)) + d + n - 1524.5);
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return $"{Day}";
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的儒略日</returns>
        public new JulianDay Next(int n)
        {
            return FromJulianDay(Day + n);
        }

        /// <summary>
        /// 公历日
        /// </summary>
        /// <returns>公历日</returns>
        public SolarDay GetSolarDay()
        {
            var d = (int)(Day + 0.5);
            var f = Day + 0.5 - d;

            if (d >= 2299161)
            {
                var c = (int)((d - 1867216.25) / 36524.25);
                d += 1 + c - (int)(c * 1D / 4);
            }

            d += 1524;
            var year = (int)((d - 122.1) / 365.25);
            d -= (int)(365.25 * year);
            var month = (int)(d * 1D / 30.601);
            d -= (int)(30.601 * month);
            var day = d;
            if (month > 13)
            {
                month -= 13;
                year -= 4715;
            }
            else
            {
                month -= 1;
                year -= 4716;
            }

            f *= 24;
            var hour = (int)f;

            f -= hour;
            f *= 60;
            var minute = (int)f;

            f -= minute;
            f *= 60;
            var second = (int)Math.Round(f);
            if (second > 59)
            {
                minute++;
            }

            if (minute > 59)
            {
                hour++;
            }

            if (hour > 23)
            {
                day += 1;
            }

            return SolarDay.FromYmd(year, month, day);
        }

        /// <summary>
        /// 公历时刻
        /// </summary>
        /// <returns>公历时刻</returns>
        public SolarTime GetSolarTime()
        {
            var d = (int)(Day + 0.5);
            var f = Day + 0.5 - d;

            if (d >= 2299161)
            {
                var c = (int)((d - 1867216.25) / 36524.25);
                d += 1 + c - (int)(c * 1D / 4);
            }

            d += 1524;
            var year = (int)((d - 122.1) / 365.25);
            d -= (int)(365.25 * year);
            var month = (int)(d * 1D / 30.601);
            d -= (int)(30.601 * month);
            var day = d;
            if (month > 13)
            {
                month -= 13;
                year -= 4715;
            }
            else
            {
                month -= 1;
                year -= 4716;
            }

            f *= 24;
            var hour = (int)f;

            f -= hour;
            f *= 60;
            var minute = (int)f;

            f -= minute;
            f *= 60;
            var second = (int)Math.Round(f);
            if (second > 59)
            {
                second -= 60;
                minute++;
            }

            if (minute > 59)
            {
                minute -= 60;
                hour++;
            }

            if (hour > 23)
            {
                hour -= 24;
                day += 1;
            }

            return SolarTime.FromYmdHms(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// 星期
        /// </summary>
        public Week Week => Week.FromIndex((int)(Day + 0.5) + 7000001);
    }
}