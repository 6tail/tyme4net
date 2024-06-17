namespace tyme.culture.nine
{
    /// <summary>
    /// 数九
    /// </summary>
    public class Nine : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "一九", "二九", "三九", "四九", "五九", "六九", "七九", "八九", "九九"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Nine(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Nine(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Nine FromIndex(int index)
        {
            return new Nine(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Nine FromName(string name)
        {
            return new Nine(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的数九</returns>
        public new Nine Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}