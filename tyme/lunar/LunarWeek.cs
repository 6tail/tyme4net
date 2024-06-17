using System;
using System.Collections.Generic;
using tyme.culture;

namespace tyme.lunar
{
    /// <summary>
    /// 农历周
    /// </summary>
    public class LunarWeek : AbstractTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names = { "第一周", "第二周", "第三周", "第四周", "第五周", "第六周" };

        /// <summary>
        /// 月
        /// </summary>
        public LunarMonth Month { get; }

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
        /// <param name="year">农历年</param>
        /// <param name="month">农历月</param>
        /// <param name="index">索引值</param>
        /// <param name="start">起始星期，1234560分别代表星期一至星期天</param>
        /// <exception cref="ArgumentException"></exception>
        public LunarWeek(int year, int month, int index, int start)
        {
            if (index < 0 || index > 5)
            {
                throw new ArgumentException($"illegal lunar week index: {index}");
            }

            if (start < 0 || start > 6)
            {
                throw new ArgumentException($"illegal lunar week start: {start}");
            }

            var m = LunarMonth.FromYm(year, month);
            if (index >= m.GetWeekCount(start))
            {
                throw new ArgumentException($"illegal lunar week index: {index} in month: {m}");
            }

            Month = m;
            Index = index;
            Start = Week.FromIndex(start);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">农历年</param>
        /// <param name="month">农历月</param>
        /// <param name="index">索引值</param>
        /// <param name="start">起始星期，1234560分别代表星期一至星期天</param>
        /// <returns>农历周</returns>
        public static LunarWeek FromYm(int year, int month, int index, int start)
        {
            return new LunarWeek(year, month, index, start);
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
            return Month + GetName();
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的农历周</returns>
        public new LunarWeek Next(int n)
        {
            if (n == 0)
            {
                return FromYm(Month.Year.Year, Month.MonthWithLeap, Index, Start.Index);
            }

            var d = Index + n;
            var m = Month;
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
                    if (!LunarDay.FromYmd(m.Year.Year, m.MonthWithLeap, 1).Week.Equals(Start))
                    {
                        d += add;
                    }
                }

                m = m.Next(add);
                if (forward)
                {
                    if (!LunarDay.FromYmd(m.Year.Year, m.MonthWithLeap, 1).Week.Equals(Start))
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

            return FromYm(m.Year.Year, m.MonthWithLeap, d, Start.Index);
        }

        /// <summary>
        /// 本周第1天
        /// </summary>
        public LunarDay FirstDay
        {
            get
            {
                var firstDay = LunarDay.FromYmd(Month.Year.Year, Month.MonthWithLeap, 1);
                return firstDay.Next(Index * 7 - IndexOf(firstDay.Week.Index - Start.Index, 7));
            }
        }

        /// <summary>
        /// 本周农历日列表
        /// </summary>
        public List<LunarDay> Days
        {
            get
            {
                var l = new List<LunarDay>(7) { FirstDay };
                for (var i = 1; i < 7; i++)
                {
                    l.Add(FirstDay.Next(i));
                }

                return l;
            }
        }
    }
}