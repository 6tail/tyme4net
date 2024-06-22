namespace tyme.culture.plumrain
{
    /// <summary>
    /// 梅雨
    /// </summary>
    public class PlumRain : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "入梅", "出梅"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public PlumRain(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public PlumRain(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static PlumRain FromIndex(int index)
        {
            return new PlumRain(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static PlumRain FromName(string name)
        {
            return new PlumRain(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的梅雨</returns>
        public new PlumRain Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}