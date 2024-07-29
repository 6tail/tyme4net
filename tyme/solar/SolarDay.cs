using System;
using tyme.culture;
using tyme.culture.dog;
using tyme.culture.nine;
using tyme.culture.phenology;
using tyme.culture.plumrain;
using tyme.festival;
using tyme.holiday;
using tyme.jd;
using tyme.lunar;

namespace tyme.solar
{
    /// <summary>
    /// 公历日
    /// </summary>
    public class SolarDay : AbstractTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "1日", "2日", "3日", "4日", "5日", "6日", "7日", "8日", "9日", "10日", "11日", "12日", "13日", "14日", "15日", "16日",
            "17日", "18日", "19日", "20日", "21日", "22日", "23日", "24日", "25日", "26日", "27日", "28日", "29日", "30日", "31日"
        };

        /// <summary>
        /// 公历月
        /// </summary>
        public SolarMonth SolarMonth { get; }

        /// <summary>
        /// 年
        /// </summary>
        public int Year => SolarMonth.Year;

        /// <summary>
        /// 月
        /// </summary>
        public int Month => SolarMonth.Month;

        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <exception cref="ArgumentException"></exception>
        public SolarDay(int year, int month, int day)
        {
            if (day < 1)
            {
                throw new ArgumentException($"illegal solar day: {year}-{month}-{day}");
            }

            var m = SolarMonth.FromYm(year, month);
            if (1582 == year && 10 == month)
            {
                if ((day > 4 && day < 15) || day > 31)
                {
                    throw new ArgumentException($"illegal solar day: {year}-{month}-{day}");
                }
            }
            else if (day > m.DayCount)
            {
                throw new ArgumentException($"illegal solar day: {year}-{month}-{day}");
            }

            SolarMonth = m;
            Day = day;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="month">公历月</param>
        /// <param name="day">公历日</param>
        /// <returns>公历日</returns>
        public static SolarDay FromYmd(int year, int month, int day)
        {
            return new SolarDay(year, month, day);
        }

        /// <summary>
        /// 星期
        /// </summary>
        public Week Week => GetJulianDay().Week;

        /// <summary>
        /// 星座
        /// </summary>
        public Constellation Constellation
        {
            get
            {
                var index = 11;
                var y = Month * 100 + Day;
                if (y >= 321 && y <= 419)
                {
                    index = 0;
                }
                else if (y >= 420 && y <= 520)
                {
                    index = 1;
                }
                else if (y >= 521 && y <= 621)
                {
                    index = 2;
                }
                else if (y >= 622 && y <= 722)
                {
                    index = 3;
                }
                else if (y >= 723 && y <= 822)
                {
                    index = 4;
                }
                else if (y >= 823 && y <= 922)
                {
                    index = 5;
                }
                else if (y >= 923 && y <= 1023)
                {
                    index = 6;
                }
                else if (y >= 1024 && y <= 1122)
                {
                    index = 7;
                }
                else if (y >= 1123 && y <= 1221)
                {
                    index = 8;
                }
                else if (y >= 1222 || y <= 119)
                {
                    index = 9;
                }
                else if (y <= 218)
                {
                    index = 10;
                }

                return Constellation.FromIndex(index);
            }
        }

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
            return SolarMonth + GetName();
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的公历日</returns>
        public new SolarDay Next(int n)
        {
            return GetJulianDay().Next(n).GetSolarDay();
        }

        /// <summary>
        /// 是否在指定公历日之前
        /// </summary>
        /// <param name="target">公历日</param>
        /// <returns>True/False</returns>
        public bool IsBefore(SolarDay target)
        {
            if (Year != target.Year)
            {
                return Year < target.Year;
            }

            return Month != target.Month ? Month < target.Month : Day < target.Day;
        }

        /// <summary>
        /// 是否在指定公历日之后
        /// </summary>
        /// <param name="target">公历日</param>
        /// <returns>True/False</returns>
        public bool IsAfter(SolarDay target)
        {
            if (Year != target.Year)
            {
                return Year > target.Year;
            }

            return Month != target.Month ? Month > target.Month : Day > target.Day;
        }

        /// <summary>
        /// 节气
        /// </summary>
        public SolarTerm Term => TermDay.SolarTerm;

        /// <summary>
        /// 节气第几天
        /// </summary>
        public SolarTermDay TermDay
        {
            get
            {
                var y = Year;
                var i = Month * 2;
                if (i == 24)
                {
                    y += 1;
                    i = 0;
                }

                var term = SolarTerm.FromIndex(y, i);
                var d = term.JulianDay.GetSolarDay();
                while (IsBefore(d))
                {
                    term = term.Next(-1);
                    d = term.JulianDay.GetSolarDay();
                }

                return new SolarTermDay(term, Subtract(d));
            }
        }

        /// <summary>
        /// 公历周
        /// </summary>
        /// <param name="start">起始星期，1234560分别代表星期一至星期天</param>
        /// <returns>公历周</returns>
        public SolarWeek GetSolarWeek(int start)
        {
            return SolarWeek.FromYm(Year, Month,
                (int)Math.Ceiling((Day + FromYmd(Year, Month, 1).Week.Next(-start).Index) / 7D) - 1, start);
        }

        /// <summary>
        /// 七十二候
        /// </summary>
        public PhenologyDay PhenologyDay
        {
            get
            {
                var dayIndex = Subtract(Term.JulianDay.GetSolarDay());
                var index = dayIndex / 5;
                if (index > 2)
                {
                    index = 2;
                }

                dayIndex -= index * 5;
                return new PhenologyDay(Phenology.FromIndex(Term.Index * 3 + index), dayIndex);
            }
        }

        /// <summary>
        /// 三伏天
        /// </summary>
        public DogDay DogDay
        {
            get
            {
                var xiaZhi = SolarTerm.FromIndex(Year, 12);
                // 第1个庚日
                var start = xiaZhi.JulianDay.GetSolarDay();
                var add = 6 - start.GetLunarDay().SixtyCycle.HeavenStem.Index;
                if (add < 0)
                {
                    add += 10;
                }

                // 第3个庚日，即初伏第1天
                add += 20;
                start = start.Next(add);
                var days = Subtract(start);
                // 初伏以前
                if (days < 0)
                {
                    return null;
                }

                if (days < 10)
                {
                    return new DogDay(Dog.FromIndex(0), days);
                }

                // 第4个庚日，中伏第1天
                start = start.Next(10);
                days = Subtract(start);
                if (days < 10)
                {
                    return new DogDay(Dog.FromIndex(1), days);
                }

                // 第5个庚日，中伏第11天或末伏第1天
                start = start.Next(10);
                days = Subtract(start);
                // 立秋
                if (xiaZhi.Next(3).JulianDay.GetSolarDay().IsAfter(start))
                {
                    if (days < 10)
                    {
                        return new DogDay(Dog.FromIndex(1), days + 10);
                    }

                    start = start.Next(10);
                    days = Subtract(start);
                }

                return days < 10 ? new DogDay(Dog.FromIndex(2), days) : null;
            }
        }

        /// <summary>
        /// 数九天
        /// </summary>
        public NineDay NineDay
        {
            get
            {
                var year = Year;
                var start = SolarTerm.FromIndex(year + 1, 0).JulianDay.GetSolarDay();
                if (IsBefore(start))
                {
                    start = SolarTerm.FromIndex(year, 0).JulianDay.GetSolarDay();
                }

                var end = start.Next(81);
                if (IsBefore(start) || !IsBefore(end))
                {
                    return null;
                }

                var days = Subtract(start);
                return new NineDay(Nine.FromIndex(days / 9), days % 9);
            }
        }

        /// <summary>
        /// 梅雨天（芒种后的第1个丙日入梅，小暑后的第1个未日出梅）
        /// </summary>
        public PlumRainDay PlumRainDay
        {
            get
            {
                // 芒种
                var grainInEar = SolarTerm.FromIndex(Year, 11);
                var start = grainInEar.JulianDay.GetSolarDay();
                var add = 2 - start.GetLunarDay().SixtyCycle.HeavenStem.Index;
                if (add < 0)
                {
                    add += 10;
                }

                // 芒种后的第1个丙日
                start = start.Next(add);

                // 小暑
                var slightHeat = grainInEar.Next(2);
                var end = slightHeat.JulianDay.GetSolarDay();
                add = 7 - end.GetLunarDay().SixtyCycle.EarthBranch.Index;
                if (add < 0)
                {
                    add += 12;
                }

                // 小暑后的第1个未日
                end = end.Next(add);

                if (IsBefore(start) || IsAfter(end))
                {
                    return null;
                }

                return Equals(end)
                    ? new PlumRainDay(PlumRain.FromIndex(1), 0)
                    : new PlumRainDay(PlumRain.FromIndex(0), Subtract(start));
            }
        }

        /// <summary>
        /// 位于当年的索引
        /// </summary>
        public int IndexInYear => Subtract(FromYmd(Year, 1, 1));

        /// <summary>
        /// 公历日期相减，获得相差天数
        /// </summary>
        /// <param name="target">公历</param>
        /// <returns>天数</returns>
        public int Subtract(SolarDay target)
        {
            return (int)GetJulianDay().Subtract(target.GetJulianDay());
        }

        /// <summary>
        /// 儒略日
        /// </summary>
        public JulianDay GetJulianDay()
        {
            return JulianDay.FromYmdHms(Year, Month, Day, 0, 0, 0);
        }

        /// <summary>
        /// 农历日
        /// </summary>
        public LunarDay GetLunarDay()
        {
            var m = LunarMonth.FromYm(Year, Month);
            var days = Subtract(m.FirstJulianDay.GetSolarDay());
            while (days < 0)
            {
                m = m.Next(-1);
                days += m.DayCount;
            }

            return LunarDay.FromYmd(m.Year, m.MonthWithLeap, days + 1);
        }

        /// <summary>
        /// 法定假日，如果当天不是法定假日，返回null
        /// </summary>
        public LegalHoliday LegalHoliday => LegalHoliday.FromYmd(Year, Month, Day);

        /// <summary>
        /// 公历现代节日，如果当天不是公历现代节日，返回null
        /// </summary>
        public SolarFestival Festival => SolarFestival.FromYmd(Year, Month, Day);
    }
}