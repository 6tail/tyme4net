using System;
using System.Collections.Generic;
using tyme.culture;
using tyme.culture.star.nine;
using tyme.culture.star.twelve;
using tyme.culture.star.twentyeight;
using tyme.lunar;
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
            var solarYear = solarDay.Year;
            var springSolarDay = SolarTerm.FromIndex(solarYear, 3).JulianDay.GetSolarDay();
            var lunarDay = solarDay.GetLunarDay();
            var lunarYear = lunarDay.LunarMonth.LunarYear;
            if (lunarYear.Year == solarYear)
            {
                if (solarDay.IsBefore(springSolarDay))
                {
                    lunarYear = lunarYear.Next(-1);
                }
            }
            else if (lunarYear.Year < solarYear)
            {
                if (!solarDay.IsBefore(springSolarDay))
                {
                    lunarYear = lunarYear.Next(1);
                }
            }

            var term = solarDay.Term;
            int index = term.Index - 3;
            if (index < 0 && term.JulianDay.GetSolarDay().IsAfter(springSolarDay))
            {
                index += 24;
            }

            SolarDay = solarDay;
            SixtyCycleMonth = new SixtyCycleMonth(SixtyCycleYear.FromYear(lunarYear.Year), LunarMonth.FromYm(solarYear, 1).SixtyCycle.Next((int)Math.Floor(index * 0.5)));
            Day = lunarDay.SixtyCycle;
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
        public TwelveStar TwelveStar =>
            TwelveStar.FromIndex(Day.EarthBranch.Index + (8 - Month.EarthBranch.Index % 6) * 2);

        /// <summary>
        /// 九星
        /// </summary>
        public NineStar NineStar
        {
            get
            {
                var dongZhi = SolarTerm.FromIndex(SolarDay.Year, 0);
                var dongZhiSolar = dongZhi.JulianDay.GetSolarDay();
                var xiaZhiSolar = dongZhi.Next(12).JulianDay.GetSolarDay();
                var dongZhiSolar2 = dongZhi.Next(24).JulianDay.GetSolarDay();
                var dongZhiIndex = dongZhiSolar.GetLunarDay().SixtyCycle.Index;
                var xiaZhiIndex = xiaZhiSolar.GetLunarDay().SixtyCycle.Index;
                var dongZhiIndex2 = dongZhiSolar2.GetLunarDay().SixtyCycle.Index;
                var solarShunBai = dongZhiSolar.Next(dongZhiIndex > 29 ? 60 - dongZhiIndex : -dongZhiIndex);
                var solarShunBai2 = dongZhiSolar2.Next(dongZhiIndex2 > 29 ? 60 - dongZhiIndex2 : -dongZhiIndex2);
                var solarNiZi = xiaZhiSolar.Next(xiaZhiIndex > 29 ? 60 - xiaZhiIndex : -xiaZhiIndex);
                var offset = 0;
                if (!SolarDay.IsBefore(solarShunBai) && SolarDay.IsBefore(solarNiZi))
                {
                    offset = SolarDay.Subtract(solarShunBai);
                }
                else if (!SolarDay.IsBefore(solarNiZi) && SolarDay.IsBefore(solarShunBai2))
                {
                    offset = 8 - SolarDay.Subtract(solarNiZi);
                }
                else if (!SolarDay.IsBefore(solarShunBai2))
                {
                    offset = SolarDay.Subtract(solarShunBai2);
                }
                else if (SolarDay.IsBefore(solarShunBai))
                {
                    offset = 8 + solarShunBai.Subtract(SolarDay);
                }

                return NineStar.FromIndex(offset);
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
    }
}