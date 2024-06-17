namespace tyme.culture.star.nine
{
    /// <summary>
    /// 九星
    /// </summary>
    public class NineStar : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "一", "二", "三", "四", "五", "六", "七", "八", "九"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public NineStar(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public NineStar(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static NineStar FromIndex(int index)
        {
            return new NineStar(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static NineStar FromName(string name)
        {
            return new NineStar(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的九星</returns>
        public new NineStar Next(int n)
        {
            return FromIndex(NextIndex(n));
        }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color => new[] { "白", "黒", "碧", "绿", "黄", "白", "赤", "白", "紫" }[Index];

        /// <summary>
        /// 五行
        /// </summary>
        public Element Element => Element.FromIndex(new[] { 4, 2, 0, 0, 2, 3, 3, 2, 1 }[Index]);

        /// <summary>
        /// 北斗九星
        /// </summary>
        public Dipper Dipper => Dipper.FromIndex(Index);

        /// <summary>
        /// 方位
        /// </summary>
        public Direction Direction => Direction.FromIndex(Index);

        /// <summary>
        /// 完整描述
        /// </summary>
        /// <returns>完整描述</returns>
        public override string ToString()
        {
            return $"{GetName()}{Color}{Element}";
        }
    }
}