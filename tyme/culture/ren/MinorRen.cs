namespace tyme.culture.ren
{
    /// <summary>
    /// 小六壬
    /// </summary>
    public class MinorRen : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "大安", "留连", "速喜", "赤口", "小吉", "空亡"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public MinorRen(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public MinorRen(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static MinorRen FromIndex(int index)
        {
            return new MinorRen(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static MinorRen FromName(string name)
        {
            return new MinorRen(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的小六壬</returns>
        public new MinorRen Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 吉凶
        /// </summary>
        public Luck Luck => Luck.FromIndex(Index % 2);
        
        /// <summary>
        /// 五行
        /// </summary>
        /// <returns>五行</returns>
        public Element GetElement()
        {
            return Element.FromIndex(new[] { 0, 4, 1, 3, 0, 2 }[Index]);
        }
    }
}