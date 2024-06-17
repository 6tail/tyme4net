namespace tyme.culture
{
    /// <summary>
    /// 宫
    /// </summary>
    public class Zone : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "东", "北", "西", "南"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Zone(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Zone(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Zone FromIndex(int index)
        {
            return new Zone(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Zone FromName(string name)
        {
            return new Zone(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的宫</returns>
        public new Zone Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 方位
        /// </summary>
        public Direction Direction => Direction.FromName(GetName());

        /// <summary>
        /// 神兽
        /// </summary>
        /// <returns>神兽</returns>
        public Beast GetBeast()
        {
            return Beast.FromIndex(Index);
        }
    }
}