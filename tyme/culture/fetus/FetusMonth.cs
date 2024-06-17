using tyme.lunar;

namespace tyme.culture.fetus
{
    /// <summary>
    /// 逐月胎神（正十二月在床房，二三九十门户中，四六十一灶勿犯，五甲七子八厕凶。）
    /// </summary>
    public class FetusMonth : LoopTyme
    {
        /// <summary>
        /// 名称
        /// </summary>
        public static string[] Names =
        {
            "占房床", "占户窗", "占门堂", "占厨灶", "占房床", "占床仓", "占碓磨", "占厕户", "占门房", "占房床", "占灶炉", "占房床"
        };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index">索引值</param>
        public FetusMonth(int index) : base(Names, index)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="lunarMonth">农历月</param>
        /// <returns>逐月胎神</returns>
        public static FetusMonth FromLunarMonth(LunarMonth lunarMonth)
        {
            return lunarMonth.IsLeap ? null : new FetusMonth(lunarMonth.Month - 1);
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的逐月胎神</returns>
        public new FetusMonth Next(int n)
        {
            return new FetusMonth(NextIndex(n));
        }
    }
}