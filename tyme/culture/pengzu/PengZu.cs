using tyme.sixtycycle;

namespace tyme.culture.pengzu
{
    /// <summary>
    /// 彭祖百忌
    /// </summary>
    public class PengZu : AbstractCulture
    {
        /// <summary>
        /// 天干彭祖百忌
        /// </summary>
        public PengZuHeavenStem PengZuHeavenStem { get; }

        /// <summary>
        /// 地支彭祖百忌
        /// </summary>
        public PengZuEarthBranch PengZuEarthBranch { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sixtyCycle">干支</param>
        public PengZu(SixtyCycle sixtyCycle)
        {
            PengZuHeavenStem = PengZuHeavenStem.FromIndex(sixtyCycle.HeavenStem.Index);
            PengZuEarthBranch = PengZuEarthBranch.FromIndex(sixtyCycle.EarthBranch.Index);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sixtyCycle">干支</param>
        /// <returns>彭祖百忌</returns>
        public static PengZu FromSixtyCycle(SixtyCycle sixtyCycle)
        {
            return new PengZu(sixtyCycle);
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return $"{PengZuHeavenStem} {PengZuEarthBranch}";
        }
    }
}