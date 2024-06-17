namespace tyme.culture.star.twelve
{
    /// <summary>
    /// 黄道黑道
    /// </summary>
    public class Ecliptic : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "黄道", "黑道"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Ecliptic(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Ecliptic(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Ecliptic FromIndex(int index)
        {
            return new Ecliptic(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Ecliptic FromName(string name)
        {
            return new Ecliptic(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的黄道黑道</returns>
        public new Ecliptic Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 吉凶
        /// </summary>
        public Luck Luck => Luck.FromIndex(Index);
    }
}