using System;
using System.Text.RegularExpressions;
using tyme.enums;
using tyme.lunar;
using tyme.solar;

namespace tyme.festival
{
    /// <summary>
    /// 农历传统节日（依据国家标准《农历的编算和颁行》GB/T 33661-2017）
    /// </summary>
    public class LunarFestival : AbstractTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
            { "春节", "元宵节", "龙头节", "上巳节", "清明节", "端午节", "七夕节", "中元节", "中秋节", "重阳节", "冬至节", "腊八节", "除夕" };

        /// <summary>
        /// 数据
        /// </summary>
        public static string Data =
            "@0000101@0100115@0200202@0300303@04107@0500505@0600707@0700715@0800815@0900909@10124@1101208@122";

        /// <summary>
        /// 类型
        /// </summary>
        public FestivalType Type { get; }

        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 农历日
        /// </summary>
        public LunarDay Day { get; }

        /// <summary>
        /// 节气
        /// </summary>
        public SolarTerm SolarTerm { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="type">节日类型</param>
        /// <param name="day">农历日</param>
        /// <param name="solarTerm">节气</param>
        /// <param name="data">数据</param>
        public LunarFestival(FestivalType type, LunarDay day, SolarTerm solarTerm, string data)
        {
            Type = type;
            Day = day;
            SolarTerm = solarTerm;
            Index = int.Parse(data.Substring(1, 2));
            Name = Names[Index];
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="index">索引值</param>
        /// <returns>农历传统节日</returns>
        /// <exception cref="ArgumentException"></exception>
        public static LunarFestival FromIndex(int year, int index)
        {
            if (index < 0 || index >= Names.Length)
            {
                throw new ArgumentException($"illegal index: {index}");
            }

            var matcher = Regex.Match(Data, $@"@{index:D2}\d+");
            if (!matcher.Success)
            {
                return null;
            }

            var data = matcher.Value;
            var type = data[3] - '0';

            switch (type)
            {
                case 0:
                    return new LunarFestival(FestivalType.Day,
                        LunarDay.FromYmd(year, int.Parse(data.Substring(4, 2)), int.Parse(data.Substring(6))), null,
                        data);
                case 1:
                    var solarTerm = SolarTerm.FromIndex(year, int.Parse(data.Substring(4)));
                    return new LunarFestival(FestivalType.Term, solarTerm.JulianDay.GetSolarDay().GetLunarDay(),
                        solarTerm, data);
                case 2:
                    return new LunarFestival(FestivalType.Eve, LunarDay.FromYmd(year + 1, 1, 1).Next(-1), null, data);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">农历年</param>
        /// <param name="month">农历月</param>
        /// <param name="day">农历日</param>
        /// <returns>农历传统节日</returns>
        public static LunarFestival FromYmd(int year, int month, int day)
        {
            var matcher = Regex.Match(Data, $@"@\d{{2}}0{month:D2}{day:D2}");
            if (matcher.Success)
            {
                return new LunarFestival(FestivalType.Day, LunarDay.FromYmd(year, month, day), null, matcher.Value);
            }

            var matches = Regex.Matches(Data, @"@\d{2}1\d{2}");
            foreach (Match match in matches)
            {
                var data = match.Value;
                var solarTerm = SolarTerm.FromIndex(year, int.Parse(data.Substring(4)));
                var d = solarTerm.JulianDay.GetSolarDay().GetLunarDay();
                if (d.Year == year && d.Month == month && d.Day == day)
                {
                    return new LunarFestival(FestivalType.Term, d, solarTerm, data);
                }
            }

            matcher = Regex.Match(Data, @"@\d{2}2");
            if (!matcher.Success)
            {
                return null;
            }

            var lunarDay = LunarDay.FromYmd(year, month, day);
            var nextDay = lunarDay.Next(1);
            return nextDay.Month == 1 && nextDay.Day == 1
                ? new LunarFestival(FestivalType.Eve, lunarDay, null, matcher.Value)
                : null;
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return Name;
        }

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return $"{Day} {Name}";
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的农历传统节日</returns>
        public new LunarFestival Next(int n)
        {
            if (n == 0)
            {
                return FromYmd(Day.Year, Day.Month, Day.Day);
            }

            var size = Names.Length;
            var t = Index + n;
            var offset = IndexOf(t, size);
            if (t < 0)
            {
                t -= size;
            }

            return FromIndex(Day.Year + t / size, offset);
        }
    }
}