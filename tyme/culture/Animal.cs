using tyme.culture.star.twentyeight;

namespace tyme.culture
{
    /// <summary>
    /// 动物
    /// </summary>
    public class Animal : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "蛟", "龙", "貉", "兔", "狐", "虎", "豹", "獬", "牛", "蝠", "鼠", "燕", "猪", "獝", "狼", "狗", "彘", "鸡", "乌", "猴", "猿",
            "犴", "羊", "獐", "马", "鹿", "蛇", "蚓"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Animal(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Animal(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Animal FromIndex(int index)
        {
            return new Animal(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Animal FromName(string name)
        {
            return new Animal(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的动物</returns>
        public new Animal Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 二十八宿
        /// </summary>
        /// <returns>二十八宿</returns>
        public TwentyEightStar GetTwentyEightStar()
        {
            return TwentyEightStar.FromIndex(Index);
        }
    }
}