namespace tyme.culture
{
    /// <summary>
    /// 月相
    /// </summary>
    public class Phase : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "朔月", "既朔月", "蛾眉新月", "蛾眉新月", "蛾眉月", "夕月", "上弦月", "上弦月", "九夜月", "宵月", "宵月", "宵月", "渐盈凸月", "小望月", "望月", "既望月",
            "立待月", "居待月", "寝待月", "更待月", "渐亏凸月", "下弦月", "下弦月", "有明月", "有明月", "蛾眉残月", "蛾眉残月", "残月", "晓月", "晦月"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Phase(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Phase(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Phase FromIndex(int index)
        {
            return new Phase(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Phase FromName(string name)
        {
            return new Phase(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的月相</returns>
        public new Phase Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}