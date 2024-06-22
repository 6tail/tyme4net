namespace tyme.culture.star.six
{
    /// <summary>
    /// 六曜（孔明六曜星、小六壬）
    /// </summary>
    public class SixStar : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "先胜", "友引", "先负", "佛灭", "大安", "赤口"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public SixStar(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public SixStar(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static SixStar FromIndex(int index)
        {
            return new SixStar(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static SixStar FromName(string name)
        {
            return new SixStar(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的六曜</returns>
        public new SixStar Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}