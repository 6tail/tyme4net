namespace tyme.culture.pengzu
{
    /// <summary>
    /// 天干彭祖百忌
    /// </summary>
    public class PengZuHeavenStem : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "甲不开仓财物耗散", "乙不栽植千株不长", "丙不修灶必见灾殃", "丁不剃头头必生疮", "戊不受田田主不祥", "己不破券二比并亡", "庚不经络织机虚张", "辛不合酱主人不尝", "壬不泱水更难提防",
            "癸不词讼理弱敌强"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public PengZuHeavenStem(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public PengZuHeavenStem(string name) : base(Names, name)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public static PengZuHeavenStem FromIndex(int index)
        {
            return new PengZuHeavenStem(index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        public static PengZuHeavenStem FromName(string name)
        {
            return new PengZuHeavenStem(name);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的天干彭祖百忌</returns>
        public new PengZuHeavenStem Next(int n)
        {
            return FromIndex(NextIndex(n));
        }
    }
}