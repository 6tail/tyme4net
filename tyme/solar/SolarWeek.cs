using System;
using System.Collections.Generic;
using tyme.unit;

namespace tyme.solar
{
    /// <summary>
    /// 公历周
    /// </summary>
    public class SolarWeek : WeekUnit
    {
        /// <summary>
        /// 公历月
        /// </summary>
        public SolarMonth SolarMonth => SolarMonth.FromYm(Year, Month);

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="month">月</param>
        /// <param name="index">索引值，0-5</param>
        /// <param name="start">起始星期，1234560分别代表星期一至星期天</param>
        /// <exception cref="ArgumentException">参数异常</exception>
        public static void Validate(int year, int month, int index, int start)
        {
            Validate(index, start);
            var m = SolarMonth.FromYm(year, month);
            if (index >= m.GetWeekCount(start))
            {
                throw new ArgumentException($"illegal solar week index: {index} in month: {m}");
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="month">公历月</param>
        /// <param name="index">索引值，0-5</param>
        /// <param name="start">起始星期，1234560分别代表星期一至星期天</param>
        /// <exception cref="ArgumentException"></exception>
        public SolarWeek(int year, int month, int index, int start)
        {
            Validate(year, month, index, start);
            Year = year;
            Month = month;
            Index = index;
            Start = start;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="month">公历月</param>
        /// <param name="index">索引值，0-5</param>
        /// <param name="start">起始星期，1234560分别代表星期一至星期天</param>
        /// <returns>公历周</returns>
        /// <exception cref="ArgumentException"></exception>
        public static SolarWeek FromYm(int year, int month, int index, int start)
        {
            return new SolarWeek(year, month, index, start);
        }

        /// <summary>
        /// 位于当年的索引
        /// </summary>
        public int IndexInYear
        {
            get
            {
                var i = 0;
                var firstDay = FirstDay;
                // 今年第1周
                var w = FromYm(Year, 1, 0, Start);
                while (!w.FirstDay.Equals(firstDay))
                {
                    w = w.Next(1);
                    i++;
                }

                return i;
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return Names[Index];
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
        /// <returns>推移后的公历周</returns>
        public new SolarWeek Next(int n)
        {
            var d = Index + n;
            var m = SolarMonth;
            if (n > 0)
            {
                var weekCount = m.GetWeekCount(Start);
                while (d >= weekCount)
                {
                    d -= weekCount;
                    m = m.Next(1);
                    if (m.FirstDay.Week.Index != Start)
                    {
                        d += 1;
                    }

                    weekCount = m.GetWeekCount(Start);
                }
            }
            else if (n < 0)
            {
                while (d < 0)
                {
                    if (m.FirstDay.Week.Index != Start)
                    {
                        d -= 1;
                    }

                    m = m.Next(-1);
                    d += m.GetWeekCount(Start);
                }
            }

            return FromYm(m.Year, m.Month, d, Start);
        }

        /// <summary>
        /// 本周第1天
        /// </summary>
        public SolarDay FirstDay
        {
            get
            {
                var firstDay = SolarDay.FromYmd(Year, Month, 1);
                return firstDay.Next(Index * 7 - IndexOf(firstDay.Week.Index - Start, 7));
            }
        }

        /// <summary>
        /// 本周公历日列表
        /// </summary>
        public List<SolarDay> Days
        {
            get
            {
                var l = new List<SolarDay>(7) { FirstDay };
                for (var i = 1; i < 7; i++)
                {
                    l.Add(FirstDay.Next(i));
                }

                return l;
            }
        }

        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="o">其他对象</param>
        /// <returns>True/False</returns>
        public override bool Equals(object o)
        {
            return o is SolarWeek week && FirstDay.Equals(week.FirstDay);
        }

        /// <summary>
        /// HashCode
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode()
        {
            return FirstDay.GetHashCode();
        }
    }
}