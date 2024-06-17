namespace tyme.culture.star.twelve
{
    /// <summary>
    /// 黄道黑道十二神
    /// </summary>
    public class TwelveStar : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "青龙", "明堂", "天刑", "朱雀", "金匮", "天德", "白虎", "玉堂", "天牢", "玄武", "司命", "勾陈"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public TwelveStar(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public TwelveStar(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static TwelveStar FromIndex(int index)
        {
            return new TwelveStar(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static TwelveStar FromName(string name)
        {
            return new TwelveStar(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的黄道黑道十二神</returns>
        public new TwelveStar Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 黄道黑道
        /// </summary>
        public Ecliptic Ecliptic => Ecliptic.FromIndex(new[] { 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1 }[Index]);
    }
}