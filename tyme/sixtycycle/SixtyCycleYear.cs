using System;
using System.Collections.Generic;
using tyme.culture;
using tyme.culture.star.nine;

namespace tyme.sixtycycle
{
    /// <summary>
    /// 干支年
    /// </summary>
    public class SixtyCycleYear : AbstractTyme
    {
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <exception cref="ArgumentException"></exception>
        public SixtyCycleYear(int year)
        {
            if (year < -1 || year > 9999)
            {
                throw new ArgumentException($"illegal sixty cycle year: {year}");
            }

            Year = year;
        }

        /// <summary>
        /// 从年初始化
        /// </summary>
        /// <param name="year">年，支持-1到9999年</param>
        /// <returns>农历年</returns>
        public static SixtyCycleYear FromYear(int year)
        {
            return new SixtyCycleYear(year);
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return $"{SixtyCycle}年";
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的年</returns>
        public new SixtyCycleYear Next(int n)
        {
            return FromYear(Year + n);
        }

        /// <summary>
        /// 干支
        /// </summary>
        public SixtyCycle SixtyCycle => SixtyCycle.FromIndex(Year - 4);

        /// <summary>
        /// 运
        /// </summary>
        public Twenty Twenty => Twenty.FromIndex((int)Math.Floor((Year - 1864) / 20D));

        /// <summary>
        /// 九星
        /// </summary>
        public NineStar NineStar => NineStar.FromIndex(63 + Twenty.Sixty.Index * 3 - SixtyCycle.Index);

        /// <summary>
        /// 太岁方位
        /// </summary>
        public Direction JupiterDirection => Direction.FromIndex(new[] { 0, 7, 7, 2, 3, 3, 8, 1, 1, 6, 0, 0 }[SixtyCycle.EarthBranch.Index]);

        /// <summary>
        /// 首月（依据五虎遁和正月起寅的规律）
        /// </summary>
        public SixtyCycleMonth FirstMonth
        {
            get
            {
                var h = HeavenStem.FromIndex((SixtyCycle.HeavenStem.Index + 1) * 2);
                return new SixtyCycleMonth(this, SixtyCycle.FromName($"{h.GetName()}寅"));
            }
        }

        /// <summary>
        /// 月份列表
        /// </summary>
        public List<SixtyCycleMonth> Months
        {
            get
            {
                var l = new List<SixtyCycleMonth>(12);
                var m = FirstMonth;
                l.Add(m);
                for (var i = 1; i < 12; i++)
                {
                    l.Add(m.Next(i));
                }
                return l;
            }
        }
    }
}