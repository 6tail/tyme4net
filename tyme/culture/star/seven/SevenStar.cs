namespace tyme.culture.star.seven
{
    /// <summary>
    /// 七曜（七政、七纬、七耀）
    /// </summary>
    public class SevenStar : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "日", "月", "火", "水", "木", "金", "土"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public SevenStar(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public SevenStar(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static SevenStar FromIndex(int index)
        {
            return new SevenStar(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static SevenStar FromName(string name)
        {
            return new SevenStar(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的七曜</returns>
        public new SevenStar Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 星期
        /// </summary>
        public Week GetWeek()
        {
            return Week.FromIndex(Index);
        }
    }
}