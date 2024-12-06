namespace tyme.sixtycycle
{
    /// <summary>
    /// 人元司令分野（地支藏干+天索引）
    /// </summary>
    public class HideHeavenStemDay : AbstractCultureDay
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="hideHeavenStem">藏干</param>
        /// <param name="dayIndex">天索引</param>
        public HideHeavenStemDay(HideHeavenStem hideHeavenStem, int dayIndex) : base(hideHeavenStem, dayIndex)
        {
        }

        /// <summary>
        /// 藏干
        /// </summary>
        public HideHeavenStem HideHeavenStem => (HideHeavenStem)culture;

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            var heavenStem = HideHeavenStem.HeavenStem;
            return heavenStem.GetName() + heavenStem.Element.GetName();
        }

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return $"{GetName()}第{DayIndex + 1}天";
        }
    }
}