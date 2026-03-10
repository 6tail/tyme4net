using System.Collections.Generic;
using tyme.culture;
using tyme.culture.star.nine;
using tyme.culture.star.twelve;
using tyme.culture.star.twentyeight;
using tyme.solar;

namespace tyme.sixtycycle
{
    /// <summary>
    /// 干支日（立春换年，节令换月）
    /// </summary>
    public class SixtyCycleDay : AbstractTyme
    {
        /// <summary>
        /// 公历日
        /// </summary>
        public SolarDay SolarDay { get; }

        /// <summary>
        /// 干支月
        /// </summary>
        public SixtyCycleMonth SixtyCycleMonth { get; }

        /// <summary>
        /// 日柱
        /// </summary>
        public SixtyCycle Day { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="solarDay">公历年</param>
        /// <param name="month">干支月</param>
        /// <param name="day">日柱</param>
        public SixtyCycleDay(SolarDay solarDay, SixtyCycleMonth month, SixtyCycle day)
        {
            SolarDay = solarDay;
            SixtyCycleMonth = month;
            Day = day;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="solarDay">公历日</param>
        public SixtyCycleDay(SolarDay solarDay)
        {
            var term = solarDay.Term;
            var index = term.Index;
            var offset = index < 3 ? index == 0 ? -2 : -1 : (index - 3) / 2;
            SolarDay = solarDay;
            SixtyCycleMonth = SixtyCycleYear.FromYear(term.Year).FirstMonth.Next(offset);
            Day = SixtyCycle.FromIndex(solarDay.Subtract(SolarDay.FromYmd(2000, 1, 7)));
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="solarDay">公历日</param>
        /// <returns>干支日</returns>
        public static SixtyCycleDay FromSolarDay(SolarDay solarDay)
        {
            return new SixtyCycleDay(solarDay);
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return $"{Day}日";
        }

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return $"{SixtyCycleMonth}{GetName()}";
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的干支日</returns>
        public new SixtyCycleDay Next(int n)
        {
            return FromSolarDay(SolarDay.Next(n));
        }

        /// <summary>
        /// 年柱
        /// </summary>
        public SixtyCycle Year => SixtyCycleMonth.Year;

        /// <summary>
        /// 月柱
        /// </summary>
        public SixtyCycle Month => SixtyCycleMonth.SixtyCycle;

        /// <summary>
        /// 干支
        /// </summary>
        public SixtyCycle SixtyCycle => Day;

        /// <summary>
        /// 建除十二值神
        /// </summary>
        public Duty Duty => Duty.FromIndex(Day.EarthBranch.Index - Month.EarthBranch.Index);

        /// <summary>
        /// 黄道黑道十二神
        /// </summary>
        public TwelveStar TwelveStar => TwelveStar.FromIndex(Day.EarthBranch.Index + (8 - Month.EarthBranch.Index % 6) * 2);

        /// <summary>
        /// 九星
        /// </summary>
        public NineStar NineStar
        {
            get
            {
                var y = SolarDay.Year;
                var winterSolstice = SolarTerm.FromIndex(y, 0).GetSolarDay();
                var summerSolstice = SolarTerm.FromIndex(y, 12).GetSolarDay();
                var nextWinterSolstice = SolarTerm.FromIndex(y + 1, 0).GetSolarDay();
                var w = winterSolstice.Next(winterSolstice.GetLunarDay().SixtyCycle.StepsCloseTo(0));
                var s = summerSolstice.Next(summerSolstice.GetLunarDay().SixtyCycle.StepsCloseTo(0));
                var n = nextWinterSolstice.Next(nextWinterSolstice.GetLunarDay().SixtyCycle.StepsCloseTo(0));
                if (SolarDay.IsBefore(w))
                {
                    return NineStar.FromIndex(w.Subtract(SolarDay) - 1);
                }

                if (SolarDay.IsBefore(s))
                {
                    return NineStar.FromIndex(SolarDay.Subtract(w));
                }

                return NineStar.FromIndex(SolarDay.IsBefore(n) ? n.Subtract(SolarDay) - 1 : SolarDay.Subtract(n));
            }
        }

        /// <summary>
        /// 太岁方位
        /// </summary>
        public Direction JupiterDirection => SixtyCycle.Index % 12 < 6 ? Element.FromIndex(SixtyCycle.Index / 12).GetDirection() : SixtyCycleMonth.SixtyCycleYear.JupiterDirection;

        /// <summary>
        /// 二十八宿
        /// </summary>
        public TwentyEightStar TwentyEightStar => TwentyEightStar.FromIndex(new[] { 10, 18, 26, 6, 14, 22, 2 }[SolarDay.Week.Index]).Next(-7 * Day.EarthBranch.Index);

        /// <summary>
        /// 当天的时辰列表
        /// </summary>
        public List<SixtyCycleHour> Hours
        {
            get
            {
                var l = new List<SixtyCycleHour>();
                var d = SolarDay.Next(-1);
                var h = SixtyCycleHour.FromSolarTime(SolarTime.FromYmdHms(d.Year, d.Month, d.Day, 23, 0, 0));
                l.Add(h);
                for (var i = 0; i < 11; i++)
                {
                    h = h.Next(7200);
                    l.Add(h);
                }

                return l;
            }
        }

        /// <summary>
        /// 神煞列表(吉神宜趋，凶神宜忌)
        /// </summary>
        /// <returns>神煞列表</returns>
        public List<God> Gods => God.GetDayGods(Month, Day);

        /// <summary>
        /// 宜
        /// </summary>
        /// <returns>宜忌列表</returns>
        public List<Taboo> Recommends => Taboo.GetDayRecommends(Month, Day);

        /// <summary>
        /// 忌
        /// </summary>
        /// <returns>宜忌列表</returns>
        public List<Taboo> Avoids => Taboo.GetDayAvoids(Month, Day);

        /// <summary>
        /// 三柱
        /// </summary>
        public ThreePillars ThreePillars => new ThreePillars(Year, Month, SixtyCycle);
    }
}