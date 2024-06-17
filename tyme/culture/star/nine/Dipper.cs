namespace tyme.culture.star.nine
{
    /// <summary>
    /// 北斗九星
    /// </summary>
    public class Dipper : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "天枢", "天璇", "天玑", "天权", "玉衡", "开阳", "摇光", "洞明", "隐元"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Dipper(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Dipper(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Dipper FromIndex(int index)
        {
            return new Dipper(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Dipper FromName(string name)
        {
            return new Dipper(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的北斗九星</returns>
        public new Dipper Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}