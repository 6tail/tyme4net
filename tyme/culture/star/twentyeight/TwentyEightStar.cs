using tyme.culture.star.seven;

namespace tyme.culture.star.twentyeight
{
    /// <summary>
    /// 二十八宿
    /// </summary>
    public class TwentyEightStar : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "角", "亢", "氐", "房", "心", "尾", "箕", "斗", "牛", "女", "虚", "危", "室", "壁", "奎", "娄", "胃", "昴", "毕", "觜", "参",
            "井", "鬼", "柳", "星", "张", "翼", "轸"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public TwentyEightStar(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public TwentyEightStar(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static TwentyEightStar FromIndex(int index)
        {
            return new TwentyEightStar(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static TwentyEightStar FromName(string name)
        {
            return new TwentyEightStar(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的二十八宿</returns>
        public new TwentyEightStar Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 七曜
        /// </summary>
        public SevenStar SevenStar => SevenStar.FromIndex(Index % 7 + 4);

        /// <summary>
        /// 九野
        /// </summary>
        public Land Land => Land.FromIndex(new[]
            { 4, 4, 4, 2, 2, 2, 7, 7, 7, 0, 0, 0, 0, 5, 5, 5, 6, 6, 6, 1, 1, 1, 8, 8, 8, 3, 3, 3 }[Index]);

        /// <summary>
        /// 宫
        /// </summary>
        public Zone Zone => Zone.FromIndex(Index / 7);

        /// <summary>
        /// 动物
        /// </summary>
        /// <returns>动物</returns>
        public Animal GetAnimal()
        {
            return Animal.FromIndex(Index);
        }

        /// <summary>
        /// 吉凶
        /// </summary>
        public Luck Luck => Luck.FromIndex(new[]
            { 0, 1, 1, 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0 }[Index]);
    }
}