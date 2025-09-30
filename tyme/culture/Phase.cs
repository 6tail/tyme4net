using System;
using tyme.jd;
using tyme.lunar;
using tyme.solar;
using tyme.util;

namespace tyme.culture
{
    /// <summary>
    /// 月相
    /// </summary>
    public class Phase : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "新月", "蛾眉月", "上弦月", "盈凸月", "满月", "亏凸月", "下弦月", "残月"
        };

        /// <summary>
        /// 农历月
        /// </summary>
        public int LunarYear { get; }

        /// <summary>
        /// 农历日
        /// </summary>
        public int LunarMonth { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="lunarYear">农历年</param>
        /// <param name="lunarMonth">农历月</param>
        /// <param name="index">索引值</param>
        public Phase(int lunarYear, int lunarMonth, int index) : base(Names, index)
        {
            var m = lunar.LunarMonth.FromYm(lunarYear, lunarMonth).Next(index / Size);
            LunarYear = m.Year;
            LunarMonth = m.MonthWithLeap;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="lunarYear">农历年</param>
        /// <param name="lunarMonth">农历月</param>
        /// <param name="name">名称</param>
        public Phase(int lunarYear, int lunarMonth, string name) : base(Names, name)
        {
            LunarYear = lunarYear;
            LunarMonth = lunarMonth;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="lunarYear">农历年</param>
        /// <param name="lunarMonth">农历月</param>
        /// <param name="index">索引值</param>
        public static Phase FromIndex(int lunarYear, int lunarMonth, int index)
        {
            return new Phase(lunarYear, lunarMonth, index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="lunarYear">农历年</param>
        /// <param name="lunarMonth">农历月</param>
        /// <param name="name">名称</param>
        public static Phase FromName(int lunarYear, int lunarMonth, string name)
        {
            return new Phase(lunarYear, lunarMonth, name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的月相</returns>
        public new Phase Next(int n)
        {
            var size = Size;
            var i = Index + n;
            if (i < 0)
            {
                i -= size;
            }

            i /= size;
            var m = lunar.LunarMonth.FromYm(LunarYear, LunarMonth);
            if (i != 0)
            {
                m = m.Next(i);
            }

            return FromIndex(m.Year, m.MonthWithLeap, NextIndex(n));
        }

        /// <summary>
        /// 公历起始时刻
        /// </summary>
        /// <returns>公历时刻</returns>
        protected SolarTime GetStartSolarTime()
        {
            var n = (int)Math.Floor((LunarYear - 2000) * 365.2422 / 29.53058886);
            var i = 0;
            var jd = JulianDay.J2000 + ShouXingUtil.OneThird;
            var d = LunarDay.FromYmd(LunarYear, LunarMonth, 1).GetSolarDay();
            double t;
            while (true)
            {
                t = ShouXingUtil.MsaLonT((n + i) * ShouXingUtil.Pi2) * 36525;
                if (!JulianDay.FromJulianDay(jd + t  - ShouXingUtil.DtT(t)).GetSolarDay().IsBefore(d))
                {
                    break;
                }

                i++;
            }

            int[] r = { 0, 90, 180, 270 };
            t = ShouXingUtil.MsaLonT((n + i + r[Index / 2] / 360D) * ShouXingUtil.Pi2) * 36525;
            return JulianDay.FromJulianDay(jd + t - ShouXingUtil.DtT(t)).GetSolarTime();
        }

        /// <summary>
        /// 公历时刻
        /// </summary>
        public SolarTime SolarTime
        {
            get
            {
                var t = GetStartSolarTime();
                return Index % 2 == 1 ? t.Next(1) : t;
            }
        }

        /// <summary>
        /// 公历日
        /// </summary>
        public SolarDay SolarDay
        {
            get
            {
                var d = GetStartSolarTime().SolarDay;
                return Index % 2 == 1 ? d.Next(1) : d;
            }
        }
    }
}