using System;
using System.Collections.Generic;
using System.Text;
using tyme.culture;
using tyme.sixtycycle;
using tyme.solar;

namespace tyme.rabbyung
{
    /// <summary>
    /// 藏历年(公历1027年为藏历元年，第一饶迥火兔年）
    /// </summary>
    public class RabByungYear : AbstractTyme
    {
        /// <summary>
        /// 饶迥(胜生周)序号，从0开始
        /// </summary>
        public int RabByungIndex { get; }

        /// <summary>
        /// 干支
        /// </summary>
        public SixtyCycle SixtyCycle { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rabByungIndex">饶迥(胜生周)序号，从0开始</param>
        /// <param name="sixtyCycle">干支</param>
        public RabByungYear(int rabByungIndex, SixtyCycle sixtyCycle)
        {
            if (rabByungIndex < 0 || rabByungIndex > 150)
            {
                throw new ArgumentException($"illegal rab-byung index: {rabByungIndex}");
            }

            RabByungIndex = rabByungIndex;
            SixtyCycle = sixtyCycle;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rabByungIndex">饶迥(胜生周)序号，从0开始</param>
        /// <param name="sixtyCycle">干支</param>
        public static RabByungYear FromSixtyCycle(int rabByungIndex, SixtyCycle sixtyCycle)
        {
            return new RabByungYear(rabByungIndex, sixtyCycle);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rabByungIndex">饶迥(胜生周)序号，从0开始</param>
        /// <param name="element">藏历五行</param>
        /// <param name="zodiac">生肖</param>
        public static RabByungYear FromElementZodiac(int rabByungIndex, RabByungElement element, Zodiac zodiac)
        {
            for (var i = 0; i < 60; i++)
            {
                var sixtyCycle = SixtyCycle.FromIndex(i);
                if (sixtyCycle.EarthBranch.GetZodiac().Equals(zodiac) && sixtyCycle.HeavenStem.Element.Index == element.Index)
                {
                    return new RabByungYear(rabByungIndex, sixtyCycle);
                }
            }

            throw new ArgumentException($"illegal rab-byung element {element}, zodiac {zodiac}");
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        public static RabByungYear FromYear(int year)
        {
            return new RabByungYear((year - 1024) / 60, SixtyCycle.FromIndex(year - 4));
        }

        /// <summary>
        /// 生肖
        /// </summary>
        public Zodiac GetZodiac()
        {
            return SixtyCycle.EarthBranch.GetZodiac();
        }

        /// <summary>
        /// 五行
        /// </summary>
        public RabByungElement Element => RabByungElement.FromIndex(SixtyCycle.HeavenStem.Element.Index);

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            string[] digits = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            string[] units = { "", "十", "百" };
            var n = RabByungIndex + 1;
            var s = new StringBuilder();
            var pos = 0;
            while (n > 0)
            {
                var digit = n % 10;
                if (digit > 0)
                {
                    s.Insert(0, digits[digit] + units[pos]);
                }
                else if (s.Length > 0)
                {
                    s.Insert(0, digits[digit]);
                }

                n /= 10;
                pos++;
            }

            var letter = s.ToString();
            if (letter.StartsWith("一十"))
            {
                letter = letter.Substring(1);
            }

            return $"第{letter}饶迥{Element}{GetZodiac()}年";
        }

        /// <summary>
        /// 年
        /// </summary>
        public int Year => 1024 + RabByungIndex * 60 + SixtyCycle.Index;

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移年数</param>
        /// <returns>推移后的藏历年</returns>
        public new RabByungYear Next(int n)
        {
            return FromYear(Year + n);
        }

        /// <summary>
        /// 闰月，1代表闰1月，0代表无闰月
        /// </summary>
        public int LeapMonth
        {
            get
            {
                var y = 1;
                var m = 4;
                var t = 0;
                var currentYear = Year;
                while (y < currentYear)
                {
                    var i = m - 1 + (t % 2 == 0 ? 33 : 32);
                    y = (y * 12 + i) / 12;
                    m = i % 12 + 1;
                    t++;
                }

                return y == currentYear ? m : 0;
            }
        }

        /// <summary>
        /// 转公历年
        /// </summary>
        /// <returns>公历年</returns>
        public SolarYear GetSolarYear()
        {
            return SolarYear.FromYear(Year);
        } 

        /// <summary>
        /// 首月
        /// </summary>
        public RabByungMonth FirstMonth => new RabByungMonth(this, 1);

        /// <summary>
        /// 月份数量
        /// </summary>
        public int MonthCount => LeapMonth < 1 ? 12 : 13;

        /// <summary>
        /// 月份列表
        /// </summary>
        public List<RabByungMonth> Months
        {
            get
            {
                var l = new List<RabByungMonth>();
                var leapMonth = LeapMonth;
                for (var i = 1; i < 13; i++)
                {
                    l.Add(new RabByungMonth(this, i));
                    if (i == leapMonth)
                    {
                        l.Add(new RabByungMonth(this, -i));
                    }
                }

                return l;
            }
        }
    }
}