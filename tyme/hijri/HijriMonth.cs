using System;
using System.Collections.Generic;
using tyme.unit;

namespace tyme.hijri
{
    /// <summary>
    /// 回历月
    /// </summary>
    public class HijriMonth : MonthUnit
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names = { "穆哈兰姆月", "色法尔月", "赖比尔·敖外鲁月", "赖比尔·阿色尼月", "主马达·敖外鲁月", "主马达·阿色尼月", "赖哲卜月", "舍尔邦月", "赖买丹月", "闪瓦鲁月", "都尔喀尔德月", "都尔黑哲月" };

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="year">回历年</param>
        /// <param name="month">回历月</param>
        /// <exception cref="ArgumentException">参数异常</exception>
        public static void Validate(int year, int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentException("illegal hijri month: " + month);
            }
            HijriYear.Validate(year);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">回历年</param>
        /// <param name="month">回历月</param>
        /// <exception cref="ArgumentException"></exception>
        public HijriMonth(int year, int month)
        {
            Validate(year, month);
            Year = year;
            Month = month;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">回历年</param>
        /// <param name="month">回历月</param>
        /// <returns>公历月</returns>
        /// <exception cref="ArgumentException"></exception>
        public static HijriMonth FromYm(int year, int month)
        {
            return new HijriMonth(year, month);
        }

        /// <summary>
        /// 回历年
        /// </summary>
        public HijriYear HijriYear => new HijriYear(Year);

        /// <summary>
        /// 天数（单数月30天，双数月29天，闰年第12月30天)
        /// </summary>
        public int DayCount
        {
            get
            {
                var d = Month % 2 == 0 ? 29 : 30;
                if (Month == 12 && HijriYear.IsLeap)
                {
                    d++;
                }
                return d;
            }
        }

        /// <summary>
        /// 位于当年的索引(0-11)
        /// </summary>
        public int IndexInYear => Month - 1;

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return Names[IndexInYear];
        }
        
        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return HijriYear + GetName();
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的回历月</returns>
        public new HijriMonth Next(int n)
        {
            var i = IndexInYear + n;
            return FromYm((Year * 12 + i) / 12, IndexOf(i, 12) + 1);
        }

        /// <summary>
        /// 本月的回历日列表
        /// </summary>
        public List<HijriDay> Days
        {
            get
            {
                var size = DayCount;
                var l = new List<HijriDay>(size);
                for (var i = 1; i <= size; i++)
                {
                    l.Add(new HijriDay(Year, Month, i));
                }

                return l;
            }
        }

        /// <summary>
        /// 首日
        /// </summary>
        public HijriDay FirstDay => new HijriDay(Year, Month, 1);
    }
}