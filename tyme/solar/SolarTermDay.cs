namespace tyme.solar
{
    /// <summary>
    /// 节气第几天
    /// </summary>
    public class SolarTermDay : AbstractCultureDay
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="solarTerm">节气</param>
        /// <param name="dayIndex">天索引</param>
        public SolarTermDay(SolarTerm solarTerm, int dayIndex) : base(solarTerm, dayIndex)
        {
        }

        /// <summary>
        /// 节气
        /// </summary>
        public SolarTerm SolarTerm => (SolarTerm)culture;
    }
}