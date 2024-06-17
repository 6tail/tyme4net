namespace tyme.culture
{
    /// <summary>
    /// 神兽
    /// </summary>
    public class Beast : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "青龙", "玄武", "白虎", "朱雀"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Beast(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Beast(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Beast FromIndex(int index)
        {
            return new Beast(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Beast FromName(string name)
        {
            return new Beast(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的神兽</returns>
        public new Beast Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 宫
        /// </summary>
        /// <returns>宫</returns>
        public Zone GetZone()
        {
            return Zone.FromIndex(Index);
        }
    }
}