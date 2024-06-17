using tyme.solar;

namespace tyme.eightchar.provider
{
    /// <summary>
    /// 童限计算接口
    /// </summary>
    public interface IChildLimitProvider
    {
        /// <summary>
        /// 童限信息
        /// </summary>
        /// <param name="birthTime">出生公历时刻</param>
        /// <param name="term">节令</param>
        /// <returns>童限信息</returns>
        ChildLimitInfo GetInfo(SolarTime birthTime, SolarTerm term);
    }
}