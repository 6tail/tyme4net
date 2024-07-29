namespace tyme.culture
{
    /// <summary>
    /// 方位
    /// </summary>
    public class Direction : LoopTyme
    {
        /// <summary>
        /// 依据后天八卦排序（0坎北, 1坤西南, 2震东, 3巽东南, 4中, 5乾西北, 6兑西, 7艮东北, 8离南）
        /// </summary>
        public static string[] Names =
        {
            "北", "西南", "东", "东南", "中", "西北", "西", "东北", "南"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Direction(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Direction(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Direction FromIndex(int index)
        {
            return new Direction(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Direction FromName(string name)
        {
            return new Direction(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的方位</returns>
        public new Direction Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 九野
        /// </summary>
        /// <returns>九野</returns>
        public Land GetLand()
        {
            return Land.FromIndex(Index);
        }

        /// <summary>
        /// 五行
        /// </summary>
        /// <returns>五行</returns>
        public Element GetElement()
        {
            return Element.FromIndex(new[] { 4, 2, 0, 0, 2, 3, 3, 2, 1 }[Index]);
        }
    }
}