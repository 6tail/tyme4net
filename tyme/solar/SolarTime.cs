using System;
using tyme.jd;
using tyme.lunar;

namespace tyme.solar
{
    /// <summary>
    /// 公历时刻
    /// </summary>
    public class SolarTime : AbstractTyme
    {
        /// <summary>
        /// 公历日
        /// </summary>
        public SolarDay Day { get; }

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
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <exception cref="ArgumentException"></exception>
        public SolarTime(int year, int month, int day, int hour, int minute, int second)
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

            Day = SolarDay.FromYmd(year, month, day);
            Hour = hour;
            Minute = minute;
            Second = second;
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
        /// <returns>公历时刻</returns>
        public static SolarTime FromYmdHms(int year, int month, int day, int hour, int minute, int second)
        {
            return new SolarTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return Hour.ToString("D2") + ":" + Minute.ToString("D2") + ":" + Second.ToString("D2");
        }

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return $"{Day} {GetName()}";
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的公历时刻</returns>
        public new SolarTime Next(int n)
        {
            if (n == 0)
            {
                return FromYmdHms(Day.Month.Year.Year, Day.Month.Month, Day.Day, Hour, Minute, Second);
            }

            var ts = Second + n;
            var tm = Minute + ts / 60;
            ts %= 60;
            if (ts < 0)
            {
                ts += 60;
                tm -= 1;
            }

            var th = Hour + tm / 60;
            tm %= 60;
            if (tm < 0)
            {
                tm += 60;
                th -= 1;
            }

            var td = th / 24;
            th %= 24;
            if (th < 0)
            {
                th += 24;
                td -= 1;
            }

            var d = Day.Next(td);
            return FromYmdHms(d.Month.Year.Year, d.Month.Month, d.Day, th, tm, ts);
        }

        /// <summary>
        /// 是否在指定公历时刻之前
        /// </summary>
        /// <param name="target">公历时刻</param>
        /// <returns>True/False</returns>
        public bool IsBefore(SolarTime target)
        {
            if (!Day.Equals(target.Day))
            {
                return Day.IsBefore(target.Day);
            }

            return Hour != target.Hour ? Hour < target.Hour :
                Minute == target.Minute ? Second < target.Second : Minute < target.Minute;
        }

        /// <summary>
        /// 是否在指定公历时刻之后
        /// </summary>
        /// <param name="target">公历时刻</param>
        /// <returns>True/False</returns>
        public bool IsAfter(SolarTime target)
        {
            if (!Day.Equals(target.Day))
            {
                return Day.IsAfter(target.Day);
            }

            return Hour != target.Hour ? Hour > target.Hour :
                Minute == target.Minute ? Second > target.Second : Minute > target.Minute;
        }

        /// <summary>
        /// 节气
        /// </summary>
        public SolarTerm Term
        {
            get
            {
                var term = SolarTerm.FromIndex(Day.Month.Year.Year + 1, 0);
                while (IsBefore(term.JulianDay.GetSolarTime()))
                {
                    term = term.Next(-1);
                }

                return term;
            }
        }

        /// <summary>
        /// 公历时刻相减，获得相差秒数
        /// </summary>
        /// <param name="target">公历时刻</param>
        /// <returns>秒数</returns>
        public int Subtract(SolarTime target)
        {
            var days = Day.Subtract(target.Day);
            var cs = Hour * 3600 + Minute * 60 + Second;
            var ts = target.Hour * 3600 + target.Minute * 60 + target.Second;
            var seconds = cs - ts;
            if (seconds < 0)
            {
                seconds += 86400;
                days--;
            }

            seconds += days * 86400;
            return seconds;
        }

        /// <summary>
        /// 儒略日
        /// </summary>
        /// <returns>儒略日</returns>
        public JulianDay GetJulianDay()
        {
            return JulianDay.FromYmdHms(Day.Month.Year.Year, Day.Month.Month, Day.Day, Hour, Minute, Second);
        }

        /// <summary>
        /// 农历时辰
        /// </summary>
        public LunarHour GetLunarHour()
        {
            var d = Day.GetLunarDay();
            return LunarHour.FromYmdHms(d.Month.Year.Year, d.Month.MonthWithLeap, d.Day, Hour, Minute, Second);
        }
    }
}