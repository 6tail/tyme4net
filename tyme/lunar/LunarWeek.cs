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
        /// 农历
        /// </summary>
        public LunarMonth LunarMonth { get; }

        /// <summary>
        /// 年
        /// </summary>
        public int Year => LunarMonth.Year;

        /// <summary>
        /// 月，闰月为负
        /// </summary>
        public int Month => LunarMonth.MonthWithLeap;

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

            LunarMonth = m;
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
            return LunarMonth + GetName();
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
                return FromYm(Year, Month, Index, Start.Index);
            }

            var d = Index + n;
            var m = LunarMonth;
            if (n > 0)
            {
                var weekCount = m.GetWeekCount(Start.Index);
                while (d >= weekCount)
                {
                    d -= weekCount;
                    m = m.Next(1);
                    if (!LunarDay.FromYmd(m.Year, m.MonthWithLeap, 1).Week.Equals(Start))
                    {
                        d += 1;
                    }

                    weekCount = m.GetWeekCount(Start.Index);
                }
            }
            else
            {
                while (d < 0)
                {
                    if (!LunarDay.FromYmd(m.Year, m.MonthWithLeap, 1).Week.Equals(Start))
                    {
                        d -= 1;
                    }

                    m = m.Next(-1);
                    d += m.GetWeekCount(Start.Index);
                }
            }

            return FromYm(m.Year, m.MonthWithLeap, d, Start.Index);
        }

        /// <summary>
        /// 本周第1天
        /// </summary>
        public LunarDay FirstDay
        {
            get
            {
                var firstDay = LunarDay.FromYmd(Year, Month, 1);
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

        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="o">其他对象</param>
        /// <returns>True/False</returns>
        public override bool Equals(object o)
        {
            return o is LunarWeek week && FirstDay.Equals(week.FirstDay);
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