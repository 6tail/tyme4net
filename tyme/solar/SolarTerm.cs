using System;
using tyme.jd;
using tyme.util;

namespace tyme.solar
{
    /// <summary>
    /// 节气
    /// </summary>
    public class SolarTerm : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "冬至", "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑", "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪"
        };

        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// 儒略日（用于日历，只精确到日中午12:00）
        /// </summary>
        public double CursoryJulianDay { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="index">索引值</param>
        public SolarTerm(int year, int index) : base(Names, index)
        {
            Year = year;
            var size = Size;
            InitByYear((year * size + index) / size, Index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="name">名称</param>
        public SolarTerm(int year, string name) : base(Names, name)
        {
            Year = year;
            InitByYear(year, Index);
        }

        /// <summary>
        /// 通过年初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="offset">偏移值</param>
        protected void InitByYear(int year, int offset)
        {
            var jd = Math.Floor((year - 2000) * 365.2422 + 180);
            // 355是2000.12冬至，得到较靠近jd的冬至估计值
            var w = Math.Floor((jd - 355 + 183) / 365.2422) * 365.2422 + 355;
            if (ShouXingUtil.CalcQi(w) > jd)
            {
                w -= 365.2422;
            }

            CursoryJulianDay = ShouXingUtil.CalcQi(w + 15.2184 * offset);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="index">索引值</param>
        /// <returns>节气</returns>
        public static SolarTerm FromIndex(int year, int index)
        {
            return new SolarTerm(year, index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="name">名称</param>
        /// <returns>节气</returns>
        public static SolarTerm FromName(int year, string name)
        {
            return new SolarTerm(year, name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的节气</returns>
        public new SolarTerm Next(int n)
        {
            var size = Size;
            var i = Index + n;
            return FromIndex((Year * size + i) / size, IndexOf(i));
        }

        /// <summary>
        /// 是否节令
        /// </summary>
        public bool IsJie => Index % 2 == 1;

        /// <summary>
        /// 是否气令
        /// </summary>
        public bool IsQi => Index % 2 == 0;

        /// <summary>
        /// 儒略日（精确到秒）
        /// </summary>
        public JulianDay JulianDay => JulianDay.FromJulianDay(ShouXingUtil.QiAccurate2(CursoryJulianDay) + JulianDay.J2000);

        /// <summary>
        /// 公历日（用于日历）
        /// </summary>
        /// <returns>公历日</returns>
        public SolarDay GetSolarDay()
        {
            return JulianDay.FromJulianDay(CursoryJulianDay + JulianDay.J2000).GetSolarDay();
        }
    }
}