namespace tyme.culture
{
    /// <summary>
    /// 五行
    /// </summary>
    public class Element : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "木", "火", "土", "金", "水"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Element(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Element(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Element FromIndex(int index)
        {
            return new Element(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Element FromName(string name)
        {
            return new Element(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的五行</returns>
        public new Element Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 我生者
        /// </summary>
        /// <returns>五行</returns>
        public Element GetReinforce()
        {
            return Next(1);
        }

        /// <summary>
        /// 我克者
        /// </summary>
        /// <returns>五行</returns>
        public Element GetRestrain()
        {
            return Next(2);
        }

        /// <summary>
        /// 生我者
        /// </summary>
        /// <returns>五行</returns>
        public Element GetReinforced()
        {
            return Next(-1);
        }

        /// <summary>
        /// 克我者
        /// </summary>
        /// <returns>五行</returns>
        public Element GetRestrained()
        {
            return Next(-2);
        }

        /// <summary>
        /// 方位
        /// </summary>
        /// <returns>方位</returns>
        public Direction GetDirection()
        {
            return Direction.FromIndex(new[] { 2, 8, 4, 6, 0 }[Index]);
        }
    }
}