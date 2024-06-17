namespace tyme.culture
{
    /// <summary>
    /// 建除十二值神
    /// </summary>
    public class Duty : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "建", "除", "满", "平", "定", "执", "破", "危", "成", "收", "开", "闭"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Duty(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Duty(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Duty FromIndex(int index)
        {
            return new Duty(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Duty FromName(string name)
        {
            return new Duty(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的值神</returns>
        public new Duty Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}