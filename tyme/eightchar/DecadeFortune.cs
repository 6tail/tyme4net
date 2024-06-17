using tyme.lunar;
using tyme.sixtycycle;

namespace tyme.eightchar
{
    /// <summary>
    /// 大运（10年1大运）
    /// </summary>
    public class DecadeFortune : AbstractTyme
    {
        /// <summary>
        /// 童限
        /// </summary>
        public ChildLimit ChildLimit { get; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="childLimit">童限</param>
        /// <param name="index">序号</param>
        public DecadeFortune(ChildLimit childLimit, int index)
        {
            ChildLimit = childLimit;
            Index = index;
        }

        /// <summary>
        /// 通过童限初始化
        /// </summary>
        /// <param name="childLimit">童限</param>
        /// <param name="index">序号</param>
        /// <returns>大运</returns>
        public static DecadeFortune FromChildLimit(ChildLimit childLimit, int index)
        {
            return new DecadeFortune(childLimit, index);
        }

        /// <summary>
        /// 开始年龄
        /// </summary>
        public int StartAge => ChildLimit.YearCount + 1 + Index * 10;

        /// <summary>
        /// 结束年龄
        /// </summary>
        public int EndAge => StartAge + 9;

        /// <summary>
        /// 开始农历年
        /// </summary>
        public LunarYear StartLunarYear => ChildLimit.EndTime.GetLunarHour().Day.Month.Year.Next(Index * 10);

        /// <summary>
        /// 结束农历年
        /// </summary>
        public LunarYear EndLunarYear => StartLunarYear.Next(9);

        /// <summary>
        /// 干支
        /// </summary>
        public SixtyCycle SixtyCycle
        {
            get
            {
                var n = Index + 1;
                return ChildLimit.EightChar.Month.Next(ChildLimit.IsForward ? n : -n);
            }
        }

        /// <summary>
        /// 开始小运
        /// </summary>
        public Fortune StartFortune => Fortune.FromChildLimit(ChildLimit, Index * 10);

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return SixtyCycle.GetName();
        }

        /// <summary>
        /// 推移
        /// </summary>
        /// <param name="n">推移步数</param>
        /// <returns>推移后的大运</returns>
        public new DecadeFortune Next(int n)
        {
            return FromChildLimit(ChildLimit, Index + n);
        }
    }
}