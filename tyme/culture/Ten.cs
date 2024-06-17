namespace tyme.culture
{
    /// <summary>
    /// 旬
    /// </summary>
    public class Ten : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "甲子", "甲戌", "甲申", "甲午", "甲辰", "甲寅"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Ten(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Ten(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Ten FromIndex(int index)
        {
            return new Ten(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Ten FromName(string name)
        {
            return new Ten(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的旬</returns>
        public new Ten Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}