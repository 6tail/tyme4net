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
        /// 五行索引，从0开始
        /// </summary>
        public int ElementIndex { get; }

        /// <summary>
        /// 生肖索引，从0开始
        /// </summary>
        public int ZodiacIndex { get; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="year">公历年</param>
        /// <exception cref="ArgumentException">参数异常</exception>
        public static void Validate(int year)
        {
            if (year < 1027 || year > 9999)
            {
                throw new ArgumentException("illegal rab-byung year: " + year);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rabByungIndex">饶迥(胜生周)序号，从0开始</param>
        /// <param name="elementIndex">五行索引，从0开始</param>
        /// <param name="zodiacIndex">生肖索引，从0开始</param>
        public RabByungYear(int rabByungIndex, int elementIndex, int zodiacIndex)
        {
            if (rabByungIndex < 0 || rabByungIndex > 150)
            {
                throw new ArgumentException("illegal rab-byung index: " + rabByungIndex);
            }

            if (elementIndex < 0 || elementIndex >= RabByungElement.Names.Length)
            {
                throw new ArgumentException("illegal element index: " + elementIndex);
            }

            if (zodiacIndex < 0 || zodiacIndex >= Zodiac.Names.Length)
            {
                throw new ArgumentException("illegal zodiac index: " + zodiacIndex);
            }

            RabByungIndex = rabByungIndex;
            ElementIndex = elementIndex;
            ZodiacIndex = zodiacIndex;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rabByungIndex">饶迥(胜生周)序号，从0开始</param>
        /// <param name="sixtyCycle">干支</param>
        public static RabByungYear FromSixtyCycle(int rabByungIndex, SixtyCycle sixtyCycle)
        {
            return new RabByungYear(rabByungIndex, sixtyCycle.HeavenStem.Element.Index, sixtyCycle.EarthBranch.GetZodiac().Index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rabByungIndex">饶迥(胜生周)序号，从0开始</param>
        /// <param name="element">藏历五行</param>
        /// <param name="zodiac">生肖</param>
        public static RabByungYear FromElementZodiac(int rabByungIndex, RabByungElement element, Zodiac zodiac)
        {
            return new RabByungYear(rabByungIndex, element.Index, zodiac.Index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        public static RabByungYear FromYear(int year)
        {
            Validate(year);
            return FromSixtyCycle((year - 1024) / 60, SixtyCycle.FromIndex(year - 4));
        }

        /// <summary>
        /// 生肖
        /// </summary>
        public Zodiac Zodiac => Zodiac.FromIndex(ZodiacIndex);

        /// <summary>
        /// 五行
        /// </summary>
        public RabByungElement Element => RabByungElement.FromIndex(ElementIndex);

        /// <summary>
        /// 干支
        /// </summary>
        public SixtyCycle SixtyCycle => SixtyCycle.FromIndex(6 * (ElementIndex * 2 + ZodiacIndex % 2) - 5 * ZodiacIndex);

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

            return $"第{letter}饶迥{Element}{Zodiac}年";
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
                var t = 1;
                var currentYear = Year;
                while (y < currentYear)
                {
                    var i = m + 31 + t;
                    y += 2;
                    m = i - 23;
                    if (i > 35)
                    {
                        y += 1;
                        m -= 12;
                    }

                    t = 1 - t;
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
        public RabByungMonth FirstMonth => new RabByungMonth(Year, 1);

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
                var y = Year;
                var leapMonth = LeapMonth;
                for (var i = 1; i < 13; i++)
                {
                    l.Add(new RabByungMonth(y, i));
                    if (i == leapMonth)
                    {
                        l.Add(new RabByungMonth(y, -i));
                    }
                }

                return l;
            }
        }
    }
}