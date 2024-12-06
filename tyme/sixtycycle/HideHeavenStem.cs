using tyme.enums;

namespace tyme.sixtycycle
{
    /// <summary>
    /// 藏干（即人元，司令取天干，分野取天干的五行）
    /// </summary>
    public class HideHeavenStem : AbstractCulture
    {
        /// <summary>
        /// 天干
        /// </summary>
        public HeavenStem HeavenStem { get; }

        /// <summary>
        /// 藏干类型
        /// </summary>
        public HideHeavenStemType Type { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="heavenStem">天干</param>
        /// <param name="type">藏干类型</param>
        public HideHeavenStem(HeavenStem heavenStem, HideHeavenStemType type)
        {
            HeavenStem = heavenStem;
            Type = type;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="heavenStemName">天干名称</param>
        /// <param name="type">藏干类型</param>
        public HideHeavenStem(string heavenStemName, HideHeavenStemType type) : this(HeavenStem.FromName(heavenStemName), type)
        {
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="heavenStemIndex">天干序号</param>
        /// <param name="type">藏干类型</param>
        public HideHeavenStem(int heavenStemIndex, HideHeavenStemType type) : this(HeavenStem.FromIndex(heavenStemIndex), type)
        {
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return HeavenStem.GetName();
        }
    }
}