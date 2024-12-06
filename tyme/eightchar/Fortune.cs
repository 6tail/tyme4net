using tyme.lunar;
using tyme.sixtycycle;

namespace tyme.eightchar
{
    /// <summary>
    /// 小运
    /// </summary>
    public class Fortune : AbstractTyme
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
        public Fortune(ChildLimit childLimit, int index)
        {
            ChildLimit = childLimit;
            Index = index;
        }

        /// <summary>
        /// 通过童限初始化
        /// </summary>
        /// <param name="childLimit">童限</param>
        /// <param name="index">序号</param>
        /// <returns>小运</returns>
        public static Fortune FromChildLimit(ChildLimit childLimit, int index)
        {
            return new Fortune(childLimit, index);
        }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age => ChildLimit.EndTime.Year - ChildLimit.StartTime.Year + 1 + Index;

        /// <summary>
        /// 农历年
        /// </summary>
        public LunarYear LunarYear => ChildLimit.EndLunarYear.Next(Index);

        /// <summary>
        /// 干支
        /// </summary>
        public SixtyCycle SixtyCycle => ChildLimit.EightChar.Hour.Next(ChildLimit.IsForward ? Age : -Age);

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
        /// <returns>推移后的小运</returns>
        public new Fortune Next(int n)
        {
            return FromChildLimit(ChildLimit, Index + n);
        }
    }
}