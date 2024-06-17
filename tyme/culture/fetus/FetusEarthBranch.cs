namespace tyme.culture.fetus
{
    /// <summary>
    /// 地支六甲胎神（《地支六甲胎神歌》子午二日碓须忌，丑未厕道莫修移。寅申火炉休要动，卯酉大门修当避。辰戌鸡栖巳亥床，犯着六甲身堕胎。）
    /// </summary>
    public class FetusEarthBranch : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "碓", "厕", "炉", "门", "栖", "床"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public FetusEarthBranch(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static FetusEarthBranch FromIndex(int index)
        {
            return new FetusEarthBranch(index);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的地支六甲胎神</returns>
        public new FetusEarthBranch Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}