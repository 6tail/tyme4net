namespace tyme.culture.dog
{
    /// <summary>
    /// 三伏
    /// </summary>
    public class Dog : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "初伏", "中伏", "末伏"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Dog(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Dog(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Dog FromIndex(int index)
        {
            return new Dog(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Dog FromName(string name)
        {
            return new Dog(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的三伏</returns>
        public new Dog Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}