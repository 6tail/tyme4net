namespace tyme.culture
{
    /// <summary>
    /// 九野
    /// </summary>
    public class Land : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "玄天", "朱天", "苍天", "阳天", "钧天", "幽天", "颢天", "变天", "炎天"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Land(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Land(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Land FromIndex(int index)
        {
            return new Land(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Land FromName(string name)
        {
            return new Land(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的九野</returns>
        public new Land Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 方位
        /// </summary>
        /// <returns>方位</returns>
        public Direction GetDirection()
        {
            return Direction.FromIndex(Index);
        }
    }
}