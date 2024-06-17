using tyme.sixtycycle;

namespace tyme.culture
{
    /// <summary>
    /// 生肖
    /// </summary>
    public class Zodiac : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Zodiac(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Zodiac(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Zodiac FromIndex(int index)
        {
            return new Zodiac(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Zodiac FromName(string name)
        {
            return new Zodiac(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的生肖</returns>
        public new Zodiac Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 地支
        /// </summary>
        public EarthBranch GetEarthBranch()
        {
            return EarthBranch.FromIndex(Index);
        }
    }
}