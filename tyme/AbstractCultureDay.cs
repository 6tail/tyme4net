namespace tyme
{
    /// <summary>
    /// 带天索引的传统文化抽象
    /// </summary>
    public abstract class AbstractCultureDay : AbstractCulture
    {
        /// <summary>
        /// 传统文化
        /// </summary>
        public readonly AbstractCulture culture;

        /// <summary>
        /// 天索引
        /// </summary>
        public int DayIndex { get; }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="culture">传统文化抽象</param>
        /// <param name="dayIndex">天索引</param>
        public AbstractCultureDay(AbstractCulture culture, int dayIndex)
        {
            this.culture = culture;
            DayIndex = dayIndex;
        }

        /// <summary>
        /// 传统文化
        /// </summary>
        protected ICulture Culture => culture; 

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return $"{culture}第{DayIndex + 1}天";
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return culture.GetName();
        }
    }
}