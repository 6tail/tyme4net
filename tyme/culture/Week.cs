using tyme.culture.star.seven;

namespace tyme.culture
{
    /// <summary>
    /// 星期
    /// </summary>
    public class Week : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "日", "一", "二", "三", "四", "五", "六"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public Week(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public Week(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static Week FromIndex(int index)
        {
            return new Week(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static Week FromName(string name)
        {
            return new Week(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的星期</returns>
        public new Week Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 七曜
        /// </summary>
        public SevenStar GetSevenStar()
        {
            return SevenStar.FromIndex(Index);
        }
    }
}