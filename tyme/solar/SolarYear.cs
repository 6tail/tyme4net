using System;
using System.Collections.Generic;

namespace tyme.solar
{
    /// <summary>
    /// 公历年
    /// </summary>
    public class SolarYear : AbstractTyme
    {
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">公历年</param>
        /// <exception cref="ArgumentException"></exception>
        public SolarYear(int year)
        {
            if (year < 1 || year > 9999)
            {
                throw new ArgumentException($"illegal solar year: {year}");
            }

            Year = year;
        }

        /// <summary>
        /// 从年初始化
        /// </summary>
        /// <param name="year">年，支持1到9999年</param>
        /// <returns>公历年</returns>
        public static SolarYear FromYear(int year)
        {
            return new SolarYear(year);
        }

        /// <summary>
        /// 天数（1582年355天，平年365天，闰年366天）
        /// </summary>
        public int DayCount
        {
            get
            {
                if (1582 == Year)
                {
                    return 355;
                }

                return IsLeap ? 366 : 365;
            }
        }

        /// <summary>
        /// 是否闰年(1582年以前，使用儒略历，能被4整除即为闰年。以后采用格里历，四年一闰，百年不闰，四百年再闰。)
        /// </summary>
        public bool IsLeap
        {
            get
            {
                if (Year < 1600)
                {
                    return Year % 4 == 0;
                }

                return (Year % 4 == 0 && Year % 100 != 0) || (Year % 400 == 0);
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return $"{Year}年";
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的公历年</returns>
        public new SolarYear Next(int n)
        {
            return FromYear(Year + n);
        }

        /// <summary>
        /// 月份列表，1年有12个月。
        /// </summary>
        public List<SolarMonth> Months
        {
            get
            {
                var l = new List<SolarMonth>(12);
                for (var i = 0; i < 12; i++)
                {
                    l.Add(SolarMonth.FromYm(Year, i + 1));
                }

                return l;
            }
        }

        /// <summary>
        /// 季度列表，1年有4个季度。
        /// </summary>
        public List<SolarSeason> Seasons
        {
            get
            {
                var l = new List<SolarSeason>(4);
                for (var i = 0; i < 4; i++)
                {
                    l.Add(SolarSeason.FromIndex(Year, i));
                }

                return l;
            }
        }

        /// <summary>
        /// 半年列表，1年有2个半年。
        /// </summary>
        public List<SolarHalfYear> HalfYears
        {
            get
            {
                var l = new List<SolarHalfYear>(2);
                for (var i = 0; i < 2; i++)
                {
                    l.Add(SolarHalfYear.FromIndex(Year, i));
                }

                return l;
            }
        }
    }
}