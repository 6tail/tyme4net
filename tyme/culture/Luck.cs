namespace tyme.culture
{
    /// <summary>
    /// 吉凶
    /// </summary>
    public class Luck : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "吉", "凶"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Luck(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Luck(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Luck FromIndex(int index)
        {
            return new Luck(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Luck FromName(string name)
        {
            return new Luck(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的吉凶</returns>
        public new Luck Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}