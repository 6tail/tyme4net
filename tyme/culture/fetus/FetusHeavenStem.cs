namespace tyme.culture.fetus
{
    /// <summary>
    /// 天干六甲胎神（《天干六甲胎神歌》甲己之日占在门，乙庚碓磨休移动。丙辛厨灶莫相干，丁壬仓库忌修弄。戊癸房床若移整，犯之孕妇堕孩童。）
    /// </summary>
    public class FetusHeavenStem : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "门", "碓磨", "厨灶", "仓库", "房床"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public FetusHeavenStem(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static FetusHeavenStem FromIndex(int index)
        {
            return new FetusHeavenStem(index);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的天干六甲胎神</returns>
        public new FetusHeavenStem Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}