using tyme.solar;

namespace tyme.eightchar
{
    /// <summary>
    /// 童限信息
    /// </summary>
    public class ChildLimitInfo
    {
        /// <summary>
        /// 开始(即出生)的公历时刻
        /// </summary>
        public SolarTime StartTime { get; }

        /// <summary>
        /// 结束(即开始起运)的公历时刻
        /// </summary>
        public SolarTime EndTime { get; }

        /// <summary>
        /// 年数
        /// </summary>
        public int YearCount { get; }

        /// <summary>
        /// 月数
        /// </summary>
        public int MonthCount { get; }

        /// <summary>
        /// 日数
        /// </summary>
        public int DayCount { get; }

        /// <summary>
        /// 小时数
        /// </summary>
        public int HourCount { get; }

        /// <summary>
        /// 分钟数
        /// </summary>
        public int MinuteCount { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="startTime">开始(即出生)的公历时刻</param>
        /// <param name="endTime">结束(即开始起运)的公历时刻</param>
        /// <param name="yearCount">年数</param>
        /// <param name="monthCount">月数</param>
        /// <param name="dayCount">日数</param>
        /// <param name="hourCount">小时数</param>
        /// <param name="minuteCount">分钟数</param>
        public ChildLimitInfo(SolarTime startTime, SolarTime endTime, int yearCount, int monthCount, int dayCount,
            int hourCount, int minuteCount)
        {
            StartTime = startTime;
            EndTime = endTime;
            YearCount = yearCount;
            MonthCount = monthCount;
            DayCount = dayCount;
            HourCount = hourCount;
            MinuteCount = minuteCount;
        }
    }
}