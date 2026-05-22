using System;
using System.Collections.Generic;
using tyme.unit;

namespace tyme.hijri
{
    /// <summary>
    /// 回历年
    /// </summary>
    public class HijriYear : YearUnit
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">回历年</param>
        /// <exception cref="ArgumentException"></exception>
        public HijriYear(int year)
        {
            Validate(year);
            Year = year;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="year">回历年</param>
        /// <exception cref="ArgumentException">参数异常</exception>
        public static void Validate(int year)
        {
            if (year < -640 || year > 9666)
            {
                throw new ArgumentException("illegal hijri year: " + year);
            }
        }

        /// <summary>
        /// 从年初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>回历年</returns>
        /// <exception cref="ArgumentException"></exception>
        public static HijriYear FromYear(int year)
        {
            return new HijriYear(year);
        }

        /// <summary>
        /// 天数（平年354天，闰年355天）
        /// </summary>
        public int DayCount => IsLeap ? 355 : 354;

        /// <summary>
        /// 是否闰年(1个闰周为30年，1个闰周中第2、5、7、10、13、16、18、21、24、26、29年为闰年)
        /// </summary>
        public bool IsLeap
        {
            get
            {
                var i = ((Year - 1) % 30 + 30) % 30;
                return i == 1 || i == 4 || i == 6 || i == 9 || i == 12 || i == 15 || i == 17 || i == 20 || i == 23 || i == 25 || i == 28;
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return Year + "年";
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的回历年</returns>
        public new HijriYear Next(int n)
        {
            return FromYear(Year + n);
        }

        /// <summary>
        /// 月份列表，1年有12个月。
        /// </summary>
        public List<HijriMonth> Months
        {
            get
            {
                var l = new List<HijriMonth>(12);
                for (var i = 1; i < 13; i++)
                {
                    l.Add(new HijriMonth(Year, i));
                }
                return l;
            }
        }

        /// <summary>
        /// 首月
        /// </summary>
        public HijriMonth FirstMonth => new HijriMonth(Year, 1);
    }
}