namespace tyme.culture
{
    /// <summary>
    /// 元（60年=1元）
    /// </summary>
    public class Sixty : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "上元", "中元", "下元"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Sixty(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Sixty(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Sixty FromIndex(int index)
        {
            return new Sixty(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Sixty FromName(string name)
        {
            return new Sixty(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的元</returns>
        public new Sixty Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}