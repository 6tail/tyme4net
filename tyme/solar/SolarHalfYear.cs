using System;
using System.Collections.Generic;

namespace tyme.solar
{
    /// <summary>
    /// 公历半年
    /// </summary>
    public class SolarHalfYear : AbstractTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names = { "上半年", "下半年" };

        /// <summary>
        /// 公历年
        /// </summary>
        public SolarYear SolarYear { get; }
        
        /// <summary>
        /// 年
        /// </summary>
        public int Year => SolarYear.Year;

        /// <summary>
        /// 索引，0-1
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="index">索引值，0-1</param>
        /// <exception cref="ArgumentException"></exception>
        public SolarHalfYear(int year, int index)
        {
            if (index < 0 || index > 1)
            {
                throw new ArgumentException($"illegal solar half year index: {index}");
            }
            SolarYear = SolarYear.FromYear(year);
            Index = index;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="index">索引值，0-1</param>
        /// <returns>公历半年</returns>
        public static SolarHalfYear FromIndex(int year, int index)
        {
            return new SolarHalfYear(year, index);
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
        /// <returns>推移后的公历半年</returns>
        public new SolarHalfYear Next(int n)
        {
            var i = Index;
            var y = Year;
            if (n != 0)
            {
                i += n;
                y += i / 2;
                i %= 2;
                if (i < 0)
                {
                    i += 2;
                    y -= 1;
                }
            }
            return FromIndex(y, i);
        }

        /// <summary>
        /// 月份列表，半年有6个月。
        /// </summary>
        public List<SolarMonth> Months
        {
            get
            {
                var l = new List<SolarMonth>(6);
                for (var i = 1; i < 7; i++)
                {
                    l.Add(SolarMonth.FromYm(Year, Index * 6 + i));
                }

                return l;
            }
        }

        /// <summary>
        /// 季度列表，半年有2个季度。
        /// </summary>
        public List<SolarSeason> Seasons
        {
            get
            {
                var l = new List<SolarSeason>(2);
                for (var i = 0; i < 2; i++)
                {
                    l.Add(SolarSeason.FromIndex(Year, Index * 2 + i));
                }

                return l;
            }
        }
    }
}