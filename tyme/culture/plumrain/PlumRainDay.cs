namespace tyme.culture.plumrain
{
    /// <summary>
    /// 梅雨天
    /// </summary>
    public class PlumRainDay : AbstractCultureDay
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="plumRain">梅雨</param>
        /// <param name="dayIndex">天索引</param>
        public PlumRainDay(PlumRain plumRain, int dayIndex) : base(plumRain, dayIndex)
        {
        }

        /// <summary>
        /// 梅雨
        /// </summary>
        public PlumRain PlumRain => (PlumRain)culture;

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return PlumRain.Index == 0 ? base.ToString() : culture.GetName();
        }
    }
}