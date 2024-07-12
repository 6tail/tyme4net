using System;
using System.Collections.Generic;
using tyme.culture;

namespace tyme.solar
{
    /// <summary>
    /// 公历周
    /// </summary>
    public class SolarWeek : AbstractTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names = { "第一周", "第二周", "第三周", "第四周", "第五周", "第六周" };

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
        /// 索引，0-5
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 起始星期
        /// </summary>
        public Week Start { get; }

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
            if (index < 0 || index > 5)
            {
                throw new ArgumentException($"illegal solar week index: {index}");
            }

            if (start < 0 || start > 6)
            {
                throw new ArgumentException($"illegal solar week start: {start}");
            }

            var m = SolarMonth.FromYm(year, month);
            if (index >= m.GetWeekCount(start))
            {
                throw new ArgumentException($"illegal solar week index: {index} in month: {m}");
            }

            SolarMonth = m;
            Index = index;
            Start = Week.FromIndex(start);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="month">公历月</param>
        /// <param name="index">索引值，0-5</param>
        /// <param name="start">起始星期，1234560分别代表星期一至星期天</param>
        /// <returns>公历周</returns>
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
                var w = FromYm(Year, 1, 0, Start.Index);
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
            if (n == 0)
            {
                return FromYm(Year, Month, Index, Start.Index);
            }

            var d = Index + n;
            var m = SolarMonth;
            var weeksInMonth = m.GetWeekCount(Start.Index);
            var forward = n > 0;
            var add = forward ? 1 : -1;
            while (forward ? (d >= weeksInMonth) : (d < 0))
            {
                if (forward)
                {
                    d -= weeksInMonth;
                }
                else
                {
                    if (!SolarDay.FromYmd(m.Year, m.Month, 1).Week.Equals(Start))
                    {
                        d += add;
                    }
                }

                m = m.Next(add);
                if (forward)
                {
                    if (!SolarDay.FromYmd(m.Year, m.Month, 1).Week.Equals(Start))
                    {
                        d += add;
                    }
                }

                weeksInMonth = m.GetWeekCount(Start.Index);
                if (!forward)
                {
                    d += weeksInMonth;
                }
            }

            return FromYm(m.Year, m.Month, d, Start.Index);
        }

        /// <summary>
        /// 本周第1天
        /// </summary>
        public SolarDay FirstDay
        {
            get
            {
                var firstDay = SolarDay.FromYmd(Year, Month, 1);
                return firstDay.Next(Index * 7 - IndexOf(firstDay.Week.Index - Start.Index, 7));
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
    }
}