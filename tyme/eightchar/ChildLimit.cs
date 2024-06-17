using tyme.eightchar.provider;
using tyme.eightchar.provider.impl;
using tyme.enums;
using tyme.solar;

namespace tyme.eightchar
{
    /// <summary>
    /// 童限
    /// </summary>
    public class ChildLimit
    {
        /// <summary>
        /// 童限计算接口
        /// </summary>
        public static IChildLimitProvider Provider = new DefaultChildLimitProvider();

        /// <summary>
        /// 八字
        /// </summary>
        public EightChar EightChar { get; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; }

        /// <summary>
        /// 顺逆
        /// </summary>
        public bool IsForward { get; }

        /// <summary>
        /// 童限信息
        /// </summary>
        public ChildLimitInfo Info { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="birthTime">出生公历时刻</param>
        /// <param name="gender">性别</param>
        public ChildLimit(SolarTime birthTime, Gender gender)
        {
            Gender = gender;
            EightChar = birthTime.GetLunarHour().EightChar;
            // 阳男阴女顺推，阴男阳女逆推
            var yang = YinYang.Yang == EightChar.Year.HeavenStem.YinYang;
            var man = Gender.Man == gender;
            IsForward = (yang && man) || (!yang && !man);
            var term = birthTime.Term;
            if (!term.IsJie)
            {
                term = term.Next(-1);
            }

            if (IsForward)
            {
                term = term.Next(2);
            }

            Info = Provider.GetInfo(birthTime, term);
        }

        /// <summary>
        /// 通过出生公历时刻初始化
        /// </summary>
        /// <param name="birthTime">出生公历时刻</param>
        /// <param name="gender">性别</param>
        /// <returns>童限</returns>
        public static ChildLimit FromSolarTime(SolarTime birthTime, Gender gender)
        {
            return new ChildLimit(birthTime, gender);
        }

        /// <summary>
        /// 年数
        /// </summary>
        public int YearCount => Info.YearCount;

        /// <summary>
        /// 月数
        /// </summary>
        public int MonthCount => Info.MonthCount;

        /// <summary>
        /// 日数
        /// </summary>
        public int DayCount => Info.DayCount;

        /// <summary>
        /// 小时数
        /// </summary>
        public int HourCount => Info.HourCount;

        /// <summary>
        /// 分钟数
        /// </summary>
        public int MinuteCount => Info.MinuteCount;

        /// <summary>
        /// 开始(即出生)的公历时刻
        /// </summary>
        public SolarTime StartTime => Info.StartTime;

        /// <summary>
        /// 结束(即开始起运)的公历时刻
        /// </summary>
        public SolarTime EndTime => Info.EndTime;

        /// <summary>
        /// 大运
        /// </summary>
        public DecadeFortune StartDecadeFortune => DecadeFortune.FromChildLimit(this, 0);

        /// <summary>
        /// 小运
        /// </summary>
        public Fortune StartFortune => Fortune.FromChildLimit(this, 0);
    }
}