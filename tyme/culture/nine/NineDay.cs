namespace tyme.culture.nine
{
    /// <summary>
    /// 数九天
    /// </summary>
    public class NineDay : AbstractCultureDay
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="nine">数九</param>
        /// <param name="dayIndex">天索引</param>
        public NineDay(Nine nine, int dayIndex) : base(nine, dayIndex)
        {
        }

        /// <summary>
        /// 数九天
        /// </summary>
        public Nine Nine => (Nine)culture;
    }
}