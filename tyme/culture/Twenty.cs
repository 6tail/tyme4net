namespace tyme.culture
{
    /// <summary>
    /// 运（20年=1运，3运=1元）
    /// </summary>
    public class Twenty : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "一运", "二运", "三运", "四运", "五运", "六运", "七运", "八运", "九运"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Twenty(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Twenty(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Twenty FromIndex(int index)
        {
            return new Twenty(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Twenty FromName(string name)
        {
            return new Twenty(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的运</returns>
        public new Twenty Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 元
        /// </summary>
        public Sixty Sixty => Sixty.FromIndex(Index / 3);
    }
}