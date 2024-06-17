namespace tyme.culture.pengzu
{
    /// <summary>
    /// 地支彭祖百忌
    /// </summary>
    public class PengZuEarthBranch : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "子不问卜自惹祸殃", "丑不冠带主不还乡", "寅不祭祀神鬼不尝", "卯不穿井水泉不香", "辰不哭泣必主重丧", "巳不远行财物伏藏", "午不苫盖屋主更张", "未不服药毒气入肠", "申不安床鬼祟入房",
            "酉不会客醉坐颠狂", "戌不吃犬作怪上床", "亥不嫁娶不利新郎"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public PengZuEarthBranch(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public PengZuEarthBranch(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static PengZuEarthBranch FromIndex(int index)
        {
            return new PengZuEarthBranch(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static PengZuEarthBranch FromName(string name)
        {
            return new PengZuEarthBranch(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的地支彭祖百忌</returns>
        public new PengZuEarthBranch Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}