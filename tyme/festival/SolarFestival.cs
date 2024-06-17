using System;
using System.Text.RegularExpressions;
using tyme.enums;
using tyme.solar;

namespace tyme.festival
{
    /// <summary>
    /// 公历现代节日
    /// </summary>
    public class SolarFestival : AbstractTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
            { "元旦", "三八妇女节", "植树节", "五一劳动节", "五四青年节", "六一儿童节", "建党节", "八一建军节", "教师节", "国庆节" };

        /// <summary>
        /// 数据
        /// </summary>
        public static string Data =
            "@00001011950@01003081950@02003121979@03005011950@04005041950@05006011950@06007011941@07008011933@08009101985@09010011950";

        /// <summary>
        /// 类型
        /// </summary>
        public FestivalType Type { get; }

        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 公历日
        /// </summary>
        public SolarDay Day { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 起始年
        /// </summary>
        public int StartYear { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="type">节日类型</param>
        /// <param name="day">公历日</param>
        /// <param name="startYear">起始年</param>
        /// <param name="data">数据</param>
        public SolarFestival(FestivalType type, SolarDay day, int startYear, string data)
        {
            Type = type;
            Day = day;
            StartYear = startYear;
            Index = int.Parse(data.Substring(1, 2));
            Name = Names[Index];
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="index">索引值</param>
        /// <returns>公历现代节日</returns>
        /// <exception cref="ArgumentException"></exception>
        public static SolarFestival FromIndex(int year, int index)
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

            if (type != FestivalType.Day.GetCode())
            {
                return null;
            }

            var startYear = int.Parse(data.Substring(8));
            return year < startYear
                ? null
                : new SolarFestival(FestivalType.Day,
                    SolarDay.FromYmd(year, int.Parse(data.Substring(4, 2)), int.Parse(data.Substring(6, 2))), startYear,
                    data);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="month">公历月</param>
        /// <param name="day">公历日</param>
        /// <returns>公历现代节日</returns>
        public static SolarFestival FromYmd(int year, int month, int day)
        {
            var matcher = Regex.Match(Data, $@"@\d{{2}}0{month:D2}{day:D2}\d+");
            if (!matcher.Success) {
                return null;
            }
            var data = matcher.Value;
            var startYear = int.Parse(data.Substring(8));
            return year < startYear ? null : new SolarFestival(FestivalType.Day, SolarDay.FromYmd(year, month, day), startYear, data);
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
        /// <returns>推移后的公历现代节日</returns>
        public new SolarFestival Next(int n)
        {
            if (n == 0) {
                return FromYmd(Day.Month.Year.Year, Day.Month.Month, Day.Day);
            }
            var size = Names.Length;
            var t = Index + n;
            var offset = IndexOf(t, size);
            if (t < 0) {
                t -= size;
            }
            return FromIndex(Day.Month.Year.Year + t / size, offset);
        }
    }
}