namespace tyme.culture
{
    /// <summary>
    /// 月相第几天
    /// </summary>
    public class PhaseDay : AbstractCultureDay
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="phase">月相</param>
        /// <param name="dayIndex">天索引</param>
        public PhaseDay(Phase phase, int dayIndex) : base(phase, dayIndex)
        {
        }

        /// <summary>
        /// 月相
        /// </summary>
        public Phase Phase => (Phase)culture;
    }
}