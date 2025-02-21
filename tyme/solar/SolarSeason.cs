using System;
using System.Collections.Generic;

namespace tyme.solar
{
    /// <summary>
    /// 公历季度
    /// </summary>
    public class SolarSeason : AbstractTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names = { "一季度", "二季度", "三季度", "四季度" };

        /// <summary>
        /// 公历年
        /// </summary>
        public SolarYear SolarYear { get; }

        /// <summary>
        /// 年
        /// </summary>
        public int Year => SolarYear.Year;

        /// <summary>
        /// 索引，0-3
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="index">索引，0-3</param>
        /// <exception cref="ArgumentException"></exception>
        public SolarSeason(int year, int index)
        {
            if (index < 0 || index > 3)
            {
                throw new ArgumentException($"illegal solar season index: {index}");
            }
            SolarYear = SolarYear.FromYear(year);
            Index = index;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="index">索引值，0-3</param>
        /// <returns>公历季度</returns>
        public static SolarSeason FromIndex(int year, int index)
        {
            return new SolarSeason(year, index);
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
            return SolarYear + GetName();
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的公历季度</returns>
        public new SolarSeason Next(int n)
        {
            var i = Index + n;
            return FromIndex((Year * 4 + i) / 4, IndexOf(i, 4));
        }

        /// <summary>
        /// 月份列表，1季度有3个月。
        /// </summary>
        public List<SolarMonth> Months
        {
            get
            {
                var l = new List<SolarMonth>(3);
                for (var i = 1; i < 4; i++)
                {
                    l.Add(SolarMonth.FromYm(Year, Index * 3 + i));
                }

                return l;
            }
        }
    }
}