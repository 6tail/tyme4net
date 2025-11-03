using System.Collections.Generic;
using tyme.culture;
using tyme.culture.star.nine;
using tyme.solar;

namespace tyme.sixtycycle
{
    /// <summary>
    /// 干支月
    /// </summary>
    public class SixtyCycleMonth : AbstractTyme
    {
        /// <summary>
        /// 干支年
        /// </summary>
        public SixtyCycleYear SixtyCycleYear { get; }

        /// <summary>
        /// 月柱
        /// </summary>
        public SixtyCycle Month { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">干支年</param>
        /// <param name="month">月柱</param>
        public SixtyCycleMonth(SixtyCycleYear year, SixtyCycle month)
        {
            SixtyCycleYear = year;
            Month = month;
        }

        /// <summary>
        /// 从年和月索引初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="index">月索引</param>
        /// <returns>干支月</returns>
        public static SixtyCycleMonth FromIndex(int year, int index)
        {
            return SixtyCycleYear.FromYear(year).FirstMonth.Next(index);
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return $"{Month}月";
        }

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return $"{SixtyCycleYear}{GetName()}";
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的干支月</returns>
        public new SixtyCycleMonth Next(int n)
        {
            return new SixtyCycleMonth(SixtyCycleYear.FromYear((SixtyCycleYear.Year * 12 + IndexInYear + n) / 12), Month.Next(n));
        }

        /// <summary>
        /// 位于当年的索引(0-11)，寅月为0，依次类推
        /// </summary>
        public int IndexInYear => Month.EarthBranch.Next(-2).Index;
        
        /// <summary>
        /// 首日（节令当天）
        /// </summary>
        public SixtyCycleDay FirstDay => SixtyCycleDay.FromSolarDay(SolarTerm.FromIndex(SixtyCycleYear.Year, 3 + IndexInYear * 2).GetSolarDay());

        /// <summary>
        /// 本月的干支日列表
        /// </summary>
        public List<SixtyCycleDay> Days
        {
            get
            {
                var l = new List<SixtyCycleDay>();
                var d = FirstDay;
                while (d.SixtyCycleMonth.Equals(this)) {
                    l.Add(d);
                    d = d.Next(1);
                }
                return l;
            }
        }
        
        /// <summary>
        /// 年柱
        /// </summary>
        public SixtyCycle Year => SixtyCycleYear.SixtyCycle;

        /// <summary>
        /// 干支
        /// </summary>
        public SixtyCycle SixtyCycle => Month;

        /// <summary>
        /// 九星
        /// </summary>
        public NineStar NineStar
        {
            get
            {
                var index = Month.EarthBranch.Index;
                if (index < 2)
                {
                    index += 3;
                }

                return NineStar.FromIndex(27 - Year.EarthBranch.Index % 3 * 3 - index);
            }
        }

        /// <summary>
        /// 太岁方位
        /// </summary>
        public Direction JupiterDirection
        {
            get
            {
                var n = new[] { 7, -1, 1, 3 }[Month.EarthBranch.Next(-2).Index % 4];
                return n == -1 ? Month.HeavenStem.Direction : Direction.FromIndex(n);
            }
        }
    }
}