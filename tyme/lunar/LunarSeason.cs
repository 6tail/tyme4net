namespace tyme.lunar
{
    /// <summary>
    /// 农历季节
    /// </summary>
    public class LunarSeason : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "孟春", "仲春", "季春", "孟夏", "仲夏", "季夏", "孟秋", "仲秋", "季秋", "孟冬", "仲冬", "季冬"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public LunarSeason(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public LunarSeason(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static LunarSeason FromIndex(int index)
        {
            return new LunarSeason(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static LunarSeason FromName(string name)
        {
            return new LunarSeason(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的农历季节</returns>
        public new LunarSeason Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}