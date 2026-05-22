using System;
using tyme.jd;
using tyme.solar;
using tyme.unit;

namespace tyme.hijri
{
    /// <summary>
    /// 回历日（公元622年7月16日为伊斯兰历元年元旦）
    /// </summary>
    public class HijriDay : DayUnit
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names = { "1日", "2日", "3日", "4日", "5日", "6日", "7日", "8日", "9日", "10日", "11日", "12日", "13日", "14日", "15日", "16日", "17日", "18日", "19日", "20日", "21日", "22日", "23日", "24日", "25日", "26日", "27日", "28日", "29日", "30日" };

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="year">回历年</param>
        /// <param name="month">回历月</param>
        /// <param name="day">回历日</param>
        /// <exception cref="ArgumentException">参数异常</exception>
        public static void Validate(int year, int month, int day)
        {
            if (day < 1 || day > HijriMonth.FromYm(year, month).DayCount)
            {
                throw new ArgumentException($"illegal hijri day: {year}-{month}-{day}");
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">回历年</param>
        /// <param name="month">回历月</param>
        /// <param name="day">回历日</param>
        public HijriDay(int year, int month, int day)
        {
            Validate(year, month, day);
            Year = year;
            Month = month;
            Day = day;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">回历年</param>
        /// <param name="month">回历月</param>
        /// <param name="day">回历日</param>
        /// <returns>回历日</returns>
        /// <exception cref="ArgumentException"></exception>
        public static HijriDay FromYmd(int year, int month, int day)
        {
            return new HijriDay(year, month, day);
        }

        /// <summary>
        /// 回历月
        /// </summary>
        public HijriMonth HijriMonth => new HijriMonth(Year, Month);

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return Names[Day - 1];
        }

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return HijriMonth + GetName();
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移天数</param>
        /// <returns>推移后的回历日</returns>
        public new HijriDay Next(int n)
        {
            return GetSolarDay().Next(n).GetHijriDay();
        }

        /// <summary>
        /// 是否在指定回历日之前
        /// </summary>
        public bool IsBefore(HijriDay target)
        {
            if (Year != target.Year)
            {
                return Year < target.Year;
            }

            return Month != target.Month ? Month < target.Month : Day < target.Day;
        }

        /// <summary>
        /// 是否在指定回历日之后
        /// </summary>
        public bool IsAfter(HijriDay target)
        {
            if (Year != target.Year)
            {
                return Year > target.Year;
            }

            return Month != target.Month ? Month > target.Month : Day > target.Day;
        }

        /// <summary>
        /// 位于当年的索引
        /// </summary>
        public int IndexInYear => Subtract(FromYmd(Year, 1, 1));

        /// <summary>
        /// 回历日期相减，获得相差天数
        /// </summary>
        public int Subtract(HijriDay target)
        {
            return (int)GetJulianDay().Subtract(target.GetJulianDay());
        }

        /// <summary>
        /// 儒略日
        /// </summary>
        public JulianDay GetJulianDay()
        {
            return new JulianDay((int)Math.Floor((11 * Year + 3) / 30.0) + 354 * Year + 30 * Month - (int)Math.Floor((Month - 1) / 2.0) + Day + 1948055);
        }

        /// <summary>
        /// 公历日
        /// </summary>
        public SolarDay GetSolarDay()
        {
            return new SolarDay(622, 7, 16).Next(Subtract(new HijriDay(1, 1, 1)));
        }
    }
}