namespace tyme.culture.phenology
{
    /// <summary>
    /// 三候
    /// </summary>
    public class ThreePhenology : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "初候", "二候", "三候"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public ThreePhenology(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public ThreePhenology(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static ThreePhenology FromIndex(int index)
        {
            return new ThreePhenology(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static ThreePhenology FromName(string name)
        {
            return new ThreePhenology(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的三候</returns>
        public new ThreePhenology Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}